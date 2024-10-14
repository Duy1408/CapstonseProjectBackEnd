﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
        private readonly IPagingServices _pagingServices;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public static int PAGE_SIZE { get; set; } = 5;

        public PropertyController(IPropertyServices pro, IPagingServices pagingServices, BlobServiceClient blobServiceClient, IMapper mapper)
        {
            _pro = pro;
            _pagingServices = pagingServices;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Property")]
        public IActionResult GetAllProperty([FromQuery] string? propertyCode, [FromQuery] Guid? zoneID, [FromQuery] Guid? blockID, [FromQuery] Guid? floorID, [FromQuery] int page = 1)
        {
            try
            {
                var propertysQuery = _pro.GetProperty().AsQueryable();

                if (propertysQuery == null || !propertysQuery.Any())
                {
                    return NotFound(new
                    {
                        message = "Property not found."
                    });
                }

                if (!string.IsNullOrEmpty(propertyCode))
                {
                    propertysQuery = propertysQuery.Where(p => p.PropertyCode.Contains(propertyCode));
                }
                if (zoneID.HasValue)
                {
                    propertysQuery = propertysQuery.Where(p => p.ZoneID == zoneID.Value);
                }
                if (blockID.HasValue)
                {
                    propertysQuery = propertysQuery.Where(p => p.BlockID == blockID.Value);
                }
                if (floorID.HasValue)
                {
                    propertysQuery = propertysQuery.Where(p => p.FloorID == floorID.Value);
                }

                var pagedPropertys = _pagingServices.GetPagedList(propertysQuery, page, PAGE_SIZE);

                if (pagedPropertys == null || !pagedPropertys.Any())
                {
                    return NotFound(new
                    {
                        message = "Property not found."
                    });
                }

                var response = _mapper.Map<List<PropertyVM>>(pagedPropertys);


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
        public IActionResult AddNewProperty(PropertyCreateDTO property)
        {
            try
            {

                var newProperty = new PropertyCreateDTO
                {
                    PropertyID = Guid.NewGuid(),
                    PropertyCode = property.PropertyCode,
                    View = property.View,
                    PriceSold = property.PriceSold,
                    Status = property.Status,
                    UnitTypeID = property.UnitTypeID,
                    FloorID = property.FloorID,
                    BlockID = property.BlockID,
                    ZoneID = property.ZoneID

                };

                var _property = _mapper.Map<Property>(newProperty);
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

                var existingProperty = _pro.GetPropertyById(id);
                if (existingProperty != null)
                {
                    if (!string.IsNullOrEmpty(property.PropertyCode))
                    {
                        existingProperty.PropertyCode = property.PropertyCode;
                    }
                    if (!string.IsNullOrEmpty(property.View))
                    {
                        existingProperty.View = property.View;
                    }
                    if (property.PriceSold.HasValue)
                    {
                        existingProperty.PriceSold = property.PriceSold.Value;
                    }
                    if (!string.IsNullOrEmpty(property.Status))
                    {
                        existingProperty.Status = property.Status;
                    }
                    if (property.UnitTypeID != null)
                    {
                        existingProperty.UnitTypeID = (Guid)property.UnitTypeID;
                    }
                    if (property.FloorID != null)
                    {
                        existingProperty.FloorID = (Guid)property.FloorID;
                    }
                    if (property.BlockID != null)
                    {
                        existingProperty.BlockID = (Guid)property.BlockID;
                    }
                    if (property.ZoneID != null)
                    {
                        existingProperty.ZoneID = (Guid)property.ZoneID;
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
