﻿using AutoMapper;
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
        private readonly IFileUploadToBlobService _fileService;

        public FloorsController(IFloorService floor, IMapper mapper, IFileUploadToBlobService fileService)
        {
            _floor = floor;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all Floor")]
        public IActionResult GetAllFloor()
        {
            try
            {
                if (_floor.GetFloors() == null)
                {
                    return NotFound(new
                    {
                        message = "Tầng không tồn tại."
                    });
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Floor By ID")]
        public IActionResult GetFloorByID(Guid id)
        {
            var floor = _floor.GetFloorById(id);

            if (floor != null)
            {
                var responese = _mapper.Map<FloorVM>(floor);
                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Tầng không tồn tại."
            });
        }

        [HttpGet("block/{blockId}")]
        [SwaggerOperation(Summary = "Get Floor By BlockID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách block.", typeof(List<FloorVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Block không tồn tại.")]
        public IActionResult GetFloorByBlockID(Guid blockId)
        {
            var floors = _floor.GetFloorByBlockID(blockId);

            if (floors != null)
            {
                var responese = _mapper.Map<List<FloorVM>>(floors);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Tầng không tồn tại."
            });

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Floor")]
        public IActionResult DeleteFloor(Guid id)
        {
            if (_floor.GetFloors() == null)
            {
                return NotFound(new
                {
                    message = "Tầng không tồn tại."
                });
            }
            var floor = _floor.GetFloorById(id);
            if (floor == null)
            {
                return NotFound(new
                {
                    message = "Tầng không tồn tại."
                });
            }

            _floor.ChangeStatus(floor);

            return Ok("Xóa tầng thành công");
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Floor")]
        public IActionResult AddNewFloor([FromForm] FloorRequestDTO floor)
        {
            try
            {

                var imageUrls = _fileService.UploadMultipleImages(floor.ImageFloor.ToList(), "floorimage");

                var newFloor = new FloorCreateDTO
                {
                    FloorID = Guid.NewGuid(),
                    NumFloor = floor.NumFloor,
                    ImageFloor = floor.ImageFloor.Count > 0 ? floor.ImageFloor.First() : null, // Store first image for reference
                    Status = true,
                    BlockID = floor.BlockID
                };

                var b = _mapper.Map<Floor>(newFloor);
                //project.Image = blobUrl;
                b.ImageFloor = string.Join(",", imageUrls); // Store all image URLs as a comma-separated string
                _floor.AddNew(b);
                return Ok(new
                {
                    message = "Tạo tầng thành công."
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
                var imageUrls = floor.ImageFloor != null ? _fileService.UploadMultipleImages(floor.ImageFloor.ToList(), "floorimage") : new List<string>();

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
                        message = "Cập nhật tầng thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Tầng không tồn tại"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
