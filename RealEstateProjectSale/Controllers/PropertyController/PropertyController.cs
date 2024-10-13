using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PropertyController
{
    [Route("api/propertys")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyServices _pro;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public PropertyController(IPropertyServices pro, BlobServiceClient blobServiceClient, IMapper mapper)
        {
            _pro = pro;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Property")]
        public IActionResult GetAllProperty()
        {
            try
            {
                if (_pro.GetProperty() == null)
                {
                    return NotFound(new
                    {
                        message = "Property not found."
                    });
                }
                var propertys = _pro.GetProperty();
                var response = _mapper.Map<List<PropertyVM>>(propertys);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Property By ID")]
        public IActionResult GetPropertyByID(Guid id)
        {
            var property = _pro.GetPropertyById(id);

            if (property != null)
            {
                var responese = _mapper.Map<PropertyVM>(property);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Property not found."
            });

        }

        [HttpGet("unitType/{unitTypeID}")]
        [SwaggerOperation(Summary = "Get Property By UnitTypeID")]
        public IActionResult GetPropertyByUnitTypeID(Guid unitTypeID)
        {
            var property = _pro.GetPropertyByUnitTypeID(unitTypeID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Property not found."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Property not found."
            });

        }

        [HttpGet("zone/{zoneID}")]
        [SwaggerOperation(Summary = "Get Property By ZoneID")]
        public IActionResult GetPropertyByZoneID(Guid zoneID)
        {
            var property = _pro.GetPropertyByZoneID(zoneID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Property not found."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Property not found."
            });

        }

        [HttpGet("block/{blockID}")]
        [SwaggerOperation(Summary = "Get Property By BlockID")]
        public IActionResult GetPropertyByBlockID(Guid blockID)
        {
            var property = _pro.GetPropertyByBlockID(blockID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Property not found."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Property not found."
            });

        }

        [HttpGet("floor/{floorID}")]
        [SwaggerOperation(Summary = "Get Property By FloorID")]
        public IActionResult GetPropertyByFloorID(Guid floorID)
        {
            var property = _pro.GetPropertyByFloorID(floorID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Property not found."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Property not found."
            });

        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Search Property By Name")]
        public ActionResult<Property> SearchPropertyByName(string searchValue)
        {
            if (_pro.GetProperty() == null)
            {
                return NotFound(new
                {
                    message = "Property not found."
                });
            }
            var property = _pro.SearchPropertyByName(searchValue);

            if (property == null)
            {
                return NotFound(new
                {
                    message = "Property not found."
                });
            }

            return Ok(property);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Property")]
        public IActionResult AddNew([FromForm] PropertyRequestDTO property)
        {
            try
            {

                var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateprojectpictures");
                string? blobUrl = null;
                if (property.Image != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{property.Image.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(property.Image.OpenReadStream());
                    var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    blobUrl = $"{storageAccountUrl}/{blobName}";
                }

                var newProperty = new PropertyCreateDTO
                {
                    PropertyID = Guid.NewGuid(),
                    PropertyName = property.PropertyName,
                    Block = property.Block,
                    Floor = property.Floor,
                    SizeArea = property.SizeArea,
                    BedRoom = property.BedRoom,
                    BathRoom = property.BathRoom,
                    LivingRoom = property.LivingRoom,
                    View = property.View,
                    InitialPrice = property.InitialPrice,
                    Discount = property.Discount,
                    MoneyTax = property.MoneyTax,
                    MaintenanceCost = property.MaintenanceCost,
                    TotalPrice = property.TotalPrice,
                    Image = property.Image,
                    Status = PropertyStatus.NotForSale.ToString(),
                    PropertyTypeID = property.PropertyTypeID,
                    ProjectID = property.ProjectID,
                };

                var _property = _mapper.Map<Property>(newProperty);
                //bug
                //_property.Image = blobUrl;
                _pro.AddNew(_property);

                return Ok(new
                {
                    message = "Create Property Successfully"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Property by ID")]
        public IActionResult UpdateProperty([FromForm] PropertyUpdateDTO property, Guid id)
        {
            try
            {

                var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateprojectpictures");
                string? blobUrl = null;
                if (property.Image != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{property.Image.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(property.Image.OpenReadStream());
                    var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    blobUrl = $"{storageAccountUrl}/{blobName}";
                }

                var existingProperty = _pro.GetPropertyById(id);
                if (existingProperty != null)
                {
                    //if (!string.IsNullOrEmpty(property.PropertyName))
                    //{
                    //    existingProperty.PropertyName = property.PropertyName;
                    //}
                    //if (!string.IsNullOrEmpty(property.Block))
                    //{
                    //    existingProperty.Block = property.Block;
                    //}
                    //if (property.Floor.HasValue)
                    //{
                    //    existingProperty.Floor = property.Floor.Value;
                    //}
                    //if (property.SizeArea.HasValue)
                    //{
                    //    existingProperty.SizeArea = property.SizeArea.Value;
                    //}
                    //if (property.BedRoom.HasValue)
                    //{
                    //    existingProperty.BedRoom = property.BedRoom.Value;
                    //}
                    //if (property.BathRoom.HasValue)
                    //{
                    //    existingProperty.BathRoom = property.BathRoom.Value;
                    //}
                    //if (property.LivingRoom.HasValue)
                    //{
                    //    existingProperty.LivingRoom = property.LivingRoom.Value;
                    //}
                    //if (!string.IsNullOrEmpty(property.View))
                    //{
                    //    existingProperty.View = property.View;
                    //}
                    //if (property.InitialPrice.HasValue)
                    //{
                    //    existingProperty.InitialPrice = property.InitialPrice.Value;
                    //}
                    //if (property.Discount.HasValue)
                    //{
                    //    existingProperty.Discount = property.Discount.Value;
                    //}
                    //if (property.MoneyTax.HasValue)
                    //{
                    //    existingProperty.MoneyTax = property.MoneyTax.Value;
                    //}
                    //if (property.MaintenanceCost.HasValue)
                    //{
                    //    existingProperty.MaintenanceCost = property.MaintenanceCost.Value;
                    //}
                    //if (property.TotalPrice.HasValue)
                    //{
                    //    existingProperty.TotalPrice = property.TotalPrice.Value;
                    //}
                    //if (blobUrl != null)
                    //{
                    //    existingProperty.Image = blobUrl;
                    //}
                    if (!string.IsNullOrEmpty(property.Status))
                    {
                        existingProperty.Status = property.Status;
                    }

                    _pro.UpdateProperty(existingProperty);

                    return Ok(new
                    {
                        message = "Update Property Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Property not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
