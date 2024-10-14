﻿using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.UnitTypeController
{
    [Route("api/unit-types")]
    [ApiController]
    public class UnitTypeController : ControllerBase
    {
        private readonly IUnitTypeServices _typeService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IMapper _mapper;

        public UnitTypeController(IUnitTypeServices typeService, BlobServiceClient blobServiceClient, IMapper mapper)
        {
            _typeService = typeService;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All UnitType")]
        public IActionResult GetAllUnitType()
        {
            try
            {
                var types = _typeService.GetAllUnitType();

                if (types == null || !types.Any())
                {
                    return NotFound();
                }

                var response = types.Select(type => new UnitTypeVM
                {
                    UnitTypeID = type.UnitTypeID,
                    BathRoom = type.BathRoom,
                    BedRoom = type.BedRoom,
                    KitchenRoom = type.KitchenRoom,
                    LivingRoom = type.LivingRoom,
                    NumberFloor = type.NumberFloor,
                    Basement = type.Basement,
                    NetFloorArea = type.NetFloorArea,
                    GrossFloorArea = type.GrossFloorArea,
                    PropertyTypeID = type.PropertyTypeID,
                    ProjectID = type.ProjectID,
                    Image = type.Image?.Split(',').ToList() ?? new List<string>(),
                    Status = type.Status
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get UnitType By ID")]
        public IActionResult GetUnitTypeByID(Guid id)
        {
            var type = _typeService.GetUnitTypeByID(id);

            if (type != null)
            {
                var response = new UnitTypeVM
                {
                    UnitTypeID = type.UnitTypeID,
                    BathRoom = type.BathRoom,
                    BedRoom = type.BedRoom,
                    KitchenRoom = type.KitchenRoom,
                    LivingRoom = type.LivingRoom,
                    NumberFloor = type.NumberFloor,
                    Basement = type.Basement,
                    NetFloorArea = type.NetFloorArea,
                    GrossFloorArea = type.GrossFloorArea,
                    PropertyTypeID = type.PropertyTypeID,
                    ProjectID = type.ProjectID,
                    Image = type.Image?.Split(',').ToList() ?? new List<string>(),
                    Status = type.Status
                };

                return Ok(response);
            }

            return NotFound(new
            {
                message = "UnitType not found."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new UnitType")]
        public IActionResult AddNewUnitType([FromForm] UnitTypeRequestDTO type)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("unittypeimage");
                var imageUrls = new List<string>(); // List to hold URLs of all images
                if (type.Image != null && type.Image.Count > 0)
                {

                    foreach (var image in type.Image)
                    {
                        var blobName = $"{Guid.NewGuid()}_{image.FileName}";
                        var blobInstance = containerInstance.GetBlobClient(blobName);
                        blobInstance.Upload(image.OpenReadStream());
                        var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/unittypeimage";

                        var blobUrl = $"{storageAccountUrl}/{blobName}";
                        imageUrls.Add(blobUrl); // Add each image URL to the list
                    }
                }

                var newCmt = new UnitTypeCreateDTO
                {
                    UnitTypeID = Guid.NewGuid(),
                    BathRoom = type.BathRoom,
                    BedRoom = type.BedRoom,
                    KitchenRoom = type.KitchenRoom,
                    LivingRoom = type.LivingRoom,
                    NumberFloor = type.NumberFloor,
                    Basement = type.Basement,
                    NetFloorArea = type.NetFloorArea,
                    GrossFloorArea = type.GrossFloorArea,
                    Status = true,
                    Image = type.Image.Count > 0 ? type.Image.First() : null
                };

                var unitType = _mapper.Map<UnitType>(newCmt);

                unitType.Image = string.Join(",", imageUrls);

                _typeService.AddNewUnitType(unitType);

                return Ok(new
                {
                    message = "Create UnitType Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update UnitType")]
        public IActionResult UpdateUnitType([FromForm] UnitTypeUpdateDTO type, Guid id)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("unittypeimage");
                var imageUrls = new List<string>(); // List to hold URLs of all images
                if (type.Image != null && type.Image.Count > 0)
                {

                    foreach (var image in type.Image)
                    {
                        var blobName = $"{Guid.NewGuid()}_{image.FileName}";
                        var blobInstance = containerInstance.GetBlobClient(blobName);
                        blobInstance.Upload(image.OpenReadStream());
                        var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/unittypeimage";
                        var blobUrl = $"{storageAccountUrl}/{blobName}";
                        imageUrls.Add(blobUrl); // Add each image URL to the list
                    }
                }

                var existingType = _typeService.GetUnitTypeByID(id);
                if (existingType != null)
                {

                    if (type.BathRoom.HasValue)
                    {
                        existingType.BathRoom = type.BathRoom.Value;
                    }
                    if (type.BedRoom.HasValue)
                    {
                        existingType.BedRoom = type.BedRoom.Value;
                    }
                    if (type.KitchenRoom.HasValue)
                    {
                        existingType.KitchenRoom = type.KitchenRoom.Value;
                    }
                    if (type.LivingRoom.HasValue)
                    {
                        existingType.LivingRoom = type.LivingRoom.Value;
                    }
                    if (type.NumberFloor.HasValue)
                    {
                        existingType.NumberFloor = type.NumberFloor.Value;
                    }
                    if (type.Basement.HasValue)
                    {
                        existingType.Basement = type.Basement.Value;
                    }
                    if (type.NetFloorArea.HasValue)
                    {
                        existingType.NetFloorArea = type.NetFloorArea.Value;
                    }
                    if (type.GrossFloorArea.HasValue)
                    {
                        existingType.GrossFloorArea = type.GrossFloorArea.Value;
                    }
                    if (imageUrls.Count > 0)
                    {
                        existingType.Image = string.Join(",", imageUrls);
                    }
                    if (type.Status.HasValue)
                    {
                        existingType.Status = type.Status.Value;
                    }



                    _typeService.UpdateUnitType(existingType);

                    return Ok(new
                    {
                        message = "Update UnitType Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "UnitType not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete UnitType by ID")]
        public IActionResult DeleteUnitType(Guid id)
        {
            if (_typeService.GetUnitTypeByID(id) == null)
            {
                return NotFound(new
                {
                    message = "UnitType not found."
                });
            }
            var type = _typeService.GetUnitTypeByID(id);
            if (type == null)
            {
                return NotFound(new
                {
                    message = "UnitType not found."
                });
            }

            _typeService.ChangeStatusUnitType(type);


            return Ok(new
            {
                message = "Delete UnitType Successfully"
            });
        }

    }
}
