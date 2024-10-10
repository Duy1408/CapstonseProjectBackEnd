using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.FloorController
{
    [Route("api/floors")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly IFloorService _floor;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public FloorsController(IFloorService floor,IMapper mapper, BlobServiceClient blobServiceClient)
        {
            _floor = floor;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
        }



        [HttpGet]
        [SwaggerOperation(Summary = "Gett all Floor")]

        public IActionResult GetAllFloor()
        {
            try
            {
                if (_floor.GetFloors() == null)
                {
                    return NotFound();
                }
                var floors = _floor.GetFloors();
                var response = _mapper.Map<List<FloorVM>>(floors);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetFloorbyID/{id}")]
        public IActionResult GetFloorByID(Guid id)
        {
            var floor = _floor.GetFloorById(id);

            if (floor != null)
            {
                var responese = _mapper.Map<FloorVM>(floor);
                return Ok(responese);
            }
            return NotFound();
        }


        [HttpDelete("DeleteFloor/{id}")]
        public IActionResult DeleteFloor(Guid id)
        {
            if (_floor.GetFloors() == null)
            {
                return NotFound();
            }
            var floor = _floor.GetFloorById(id);
            if (floor == null)
            {
                return NotFound();
            }

           _floor.ChangeStatus(floor);

            return Ok("Delete Successfully");
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Floor")]
        public IActionResult AddNewFloor([FromForm] FloorRequestDTO floor, Guid blockId)
        {
            try
            {

                var containerInstance = _blobServiceClient.GetBlobContainerClient("floorimage");
                var imageUrls = new List<string>(); // List to hold URLs of all images
                if (floor.ImageFloor != null && floor.ImageFloor.Count > 0)
                {

                    foreach (var image in floor.ImageFloor)
                    {
                        var blobName = $"{Guid.NewGuid()}_{image.FileName}";
                        var blobInstance = containerInstance.GetBlobClient(blobName);
                        blobInstance.Upload(image.OpenReadStream());
                        var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/floorimage";

                        var blobUrl = $"{storageAccountUrl}/{blobName}";
                        imageUrls.Add(blobUrl); // Add each image URL to the list
                    }
                }

                var newFloor = new FloorCreateDTO
                {
                    FloorID = Guid.NewGuid(),
                    NumFloor = floor.NumFloor,
                    ImageFloor = floor.ImageFloor.Count > 0 ? floor.ImageFloor.First() : null, // Store first image for reference
                    Status = true,
                    BlockID = blockId,
                };

                var b = _mapper.Map<Floor>(newFloor);
                //project.Image = blobUrl;
                b.ImageFloor = string.Join(",", imageUrls); // Store all image URLs as a comma-separated string
                _floor.AddNew(b);
                return Ok(new
                {
                    message = "Create Block Successfully"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "UpdateFloor")]
        public IActionResult UpdateBlock([FromForm] FloorUpdateDTO floor, Guid id)
        {
            try
            {

                var containerInstance = _blobServiceClient.GetBlobContainerClient("floorimage");
                var imageUrls = new List<string>(); // List to hold URLs of all images
                if (floor.ImageFloor != null && floor.ImageFloor.Count > 0)
                {

                    foreach (var image in floor.ImageFloor)
                    {
                        var blobName = $"{Guid.NewGuid()}_{image.FileName}";
                        var blobInstance = containerInstance.GetBlobClient(blobName);
                        blobInstance.Upload(image.OpenReadStream());
                        var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/floorimage";

                        var blobUrl = $"{storageAccountUrl}/{blobName}";
                        imageUrls.Add(blobUrl); // Add each image URL to the list
                    }
                }

                var existingFloor = _floor.GetFloorById(id);
                if (existingFloor != null)
                {

                    if (floor.NumFloor.HasValue)
                    {
                        existingFloor.NumFloor = floor.NumFloor.Value;
                    }
                    if (imageUrls.Count > 0)
                    {
                        existingFloor.ImageFloor = string.Join(",", imageUrls);
                    }
                    if (floor.Status.HasValue)
                    {
                        existingFloor.Status = floor.Status.Value;
                    }
                    if (floor.BlockID.HasValue)
                    {
                        existingFloor.BlockID = floor.BlockID.Value;
                    }
                    _floor.UpdateFloor(existingFloor);

                    return Ok(new
                    {
                        message = "Update Floor Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Floor not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
