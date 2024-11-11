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
using System.Security.Policy;
using RealEstateZone = RealEstateProjectSaleBusinessObject.BusinessObject.Zone;


namespace RealEstateProjectSale.Controllers.ZoneController
{
    [Route("api/zones")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly IZoneService _zone;
        private readonly IMapper _mapper;
        private readonly IFileUploadToBlobService _fileService;
        public ZonesController(IZoneService zone, IMapper mapper, IFileUploadToBlobService fileService)
        {
            _zone = zone;
            _mapper = mapper;
            _fileService = fileService;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Get all Zone")]

        public IActionResult GetAllZone()
        {
            try
            {
                if (_zone.GetZones() == null)
                {
                    return NotFound();
                }
                var zones = _zone.GetZones();
                var response = _mapper.Map<List<ZoneVM>>(zones);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetZonebyID/{id}")]
        public IActionResult GetZoneByID(Guid id)
        {
            var zone = _zone.GetZoneById(id);

            if (zone != null)
            {
                var responese = _mapper.Map<ZoneVM>(zone);
                return Ok(responese);
            }
            return NotFound();
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "UpdateZone")]
        public IActionResult UpdateBlock([FromForm] ZoneUpdateDTO zone, Guid id)
        {
            try
            {
                var imageUrls = zone.ImageZone != null ? _fileService.UploadMultipleImages(zone.ImageZone.ToList(), "zoneimage") : new List<string>();

                var existingZone = _zone.GetZoneById(id);
                if (existingZone != null)
                {

                    if (!string.IsNullOrEmpty(zone.ZoneName))
                    {
                        existingZone.ZoneName = zone.ZoneName;
                    }
                    if (imageUrls.Count > 0)
                    {
                        existingZone.ImageZone = string.Join(",", imageUrls);
                    }
                    if (zone.Status.HasValue)
                    {
                        existingZone.Status = zone.Status.Value;
                    }
                    if (zone.ProjectID.HasValue)
                    {
                        existingZone.ProjectID = zone.ProjectID.Value;
                    }
                    _zone.UpdateZone(existingZone);

                    return Ok(new
                    {
                        message = "Cập nhật phân khu thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Phân khu không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Zone")]
        public IActionResult AddNewZone([FromForm] ZoneRequestDTO zone, Guid projectId)
        {
            try
            {

                var imageUrls = _fileService.UploadMultipleImages(zone.ImageZone.ToList(), "zoneimage");

                var newZone = new ZoneCreateDTO
                {
                    ZoneID = Guid.NewGuid(),
                    ZoneName = zone.ZoneName,
                    ImageZone = zone.ImageZone.Count > 0 ? zone.ImageZone.First() : null, // Store first image for reference
                    Status = true,
                    ProjectID = projectId,

                };

                var z = _mapper.Map<RealEstateZone>(newZone);
                //project.Image = blobUrl;
                z.ImageZone = string.Join(",", imageUrls); // Store all image URLs as a comma-separated string
                _zone.AddNew(z);

                return Ok(new
                {
                    message = "Tạo phân khu thành công."
                });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [HttpDelete("DeleteZone/{id}")]
        public IActionResult DeleteZone(Guid id)
        {
            if (_zone.GetZones() == null)
            {
                return NotFound();
            }
            var zone = _zone.GetZoneById(id);
            if (zone == null)
            {
                return NotFound();
            }

            _zone.ChangeStatus(zone);


            return Ok("Xóa phân khu thành công.");
        }




    }
}
