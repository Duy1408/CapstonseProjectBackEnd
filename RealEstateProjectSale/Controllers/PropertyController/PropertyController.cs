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

namespace RealEstateProjectSale.Controllers.PropertyController
{
    [Route("api/[controller]")]
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
        [Route("GetAllProperty")]
        public IActionResult GetAllProperty()
        {
            try
            {
                if (_pro.GetProperty() == null)
                {
                    return NotFound();
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

        [HttpGet("GetPropertyByID/{id}")]
        public IActionResult GetPropertyByID(Guid id)
        {
            var property = _pro.GetPropertyById(id);

            if (property != null)
            {
                var responese = _mapper.Map<PropertyVM>(property);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpGet("GetPropertyByProjectID/{id}")]
        public IActionResult GetPropertyByProjectID(Guid id)
        {
            var property = _pro.GetPropertyByProjectID(id);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound();
                }

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpGet("SearchPropertyByName/{searchValue}")]
        public ActionResult<Property> SearchPropertyByName(string searchValue)
        {
            if (_pro.GetProperty() == null)
            {
                return NotFound();
            }
            var property = _pro.SearchPropertyByName(searchValue);

            if (property == null)
            {
                return NotFound("Don't have this Property ");
            }

            return Ok(property);
        }

        [HttpGet("GetPropertyByPropertyTypeID/{projectID}/{propertyTypeID}")]
        public IActionResult GetPropertyByPropertyTypeID(Guid projectID, Guid propertyTypeID)
        {

            var property = _pro.GetPropertyByPropertyTypeID(propertyTypeID)
                         .Where(p => p.ProjectID == projectID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound();
                }

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        [Route("AddNewProperty")]
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

                return Ok("Create Property Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProperty/{id}")]
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

                    return Ok("Update Property Successfully");

                }

                return NotFound("Property not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
