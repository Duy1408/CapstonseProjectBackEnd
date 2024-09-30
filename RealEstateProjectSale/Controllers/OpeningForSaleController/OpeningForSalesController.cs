using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.OpeningForSaleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningForSalesController : ControllerBase
    {
        private readonly IOpeningForSaleServices _open;
        private readonly IMapper _mapper;

        public OpeningForSalesController(IOpeningForSaleServices open, IMapper mapper)
        {
            _open = open;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllOpeningForSale")]
        public IActionResult GetAllOpeningForSale()
        {
            try
            {
                if (_open.GetOpeningForSales() == null)
                {
                    return NotFound();
                }
                var opens = _open.GetOpeningForSales();
                var response = _mapper.Map<List<OpeningForSaleVM>>(opens);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOpeningForSaleByID/{id}")]
        public IActionResult GetOpeningForSaleByID(Guid id)
        {
            var open = _open.GetOpeningForSaleById(id);

            if (open != null)
            {
                var responese = _mapper.Map<OpeningForSaleVM>(open);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpGet("GetOpeningForSaleByProjectID/{id}")]
        public IActionResult GetOpeningForSaleByProjectID(Guid id)
        {
            var open = _open.GetPropertyByProjectID(id);

            if (open != null)
            {
                var responese = open.Select(open => _mapper.Map<OpeningForSaleVM>(open)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound();
                }

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpGet("SearchOpeningForSaleByName/{descriptionName}")]
        public ActionResult<OpeningForSale> SearchOpeningForSaleByName(string descriptionName)
        {
            if (_open.GetOpeningForSales() == null)
            {
                return NotFound();
            }
            var open = _open.SearchOpeningForSale(descriptionName);

            if (open == null)
            {
                return NotFound("Don't have this Property ");
            }

            return Ok(open);
        }

        [HttpPost]
        [Route("AddNewComment")]
        public IActionResult AddNew(OpeningForSaleCreateDTO open)
        {
            try
            {
                var newOpen = new OpeningForSaleCreateDTO
                {
                    OpeningForSaleID = Guid.NewGuid(),
                    DescriptionName = open.DescriptionName,
                    DateStart = open.DateStart,
                    DateEnd = open.DateEnd,
                    ReservationTime = open.ReservationTime,
                    Description = open.Description,
                    Status = true,
                    ProjectID = open.ProjectID
                };

                var opening = _mapper.Map<OpeningForSale>(newOpen);
                _open.AddNew(opening);

                return Ok("Create OpeningForSale Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateComment/{id}")]
        public IActionResult UpdateComment([FromForm] OpeningForSaleUpdateDTO open, Guid id)
        {
            try
            {
                var existingOpen = _open.GetOpeningForSaleById(id);
                if (existingOpen != null)
                {
                    if (!string.IsNullOrEmpty(open.DescriptionName))
                    {
                        existingOpen.DescriptionName = open.DescriptionName;
                    }
                    //bug
                    //if (open.DateStart.HasValue)
                    //{
                    //    existingOpen.DateStart = open.DateStart.Value;
                    //}
                    //if (open.DateEnd.HasValue)
                    //{
                    //    existingOpen.DateEnd = open.DateEnd.Value;
                    //}
                    //if (!string.IsNullOrEmpty(open.ReservationTime))
                    //{
                    //    existingOpen.ReservationTime = open.ReservationTime;
                    //}
                    if (!string.IsNullOrEmpty(open.Description))
                    {
                        existingOpen.Description = open.Description;
                    }
                    if (open.Status.HasValue)
                    {
                        existingOpen.Status = open.Status.Value;
                    }

                    _open.UpdateOpeningForSale(existingOpen);

                    return Ok("Update OpeningForSale Successfully");

                }

                return NotFound("OpeningForSale not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOpeningForSale/{id}")]
        public IActionResult DeleteOpeningForSale(Guid id)
        {
            if (_open.GetOpeningForSaleById(id) == null)
            {
                return NotFound();
            }
            var open = _open.GetOpeningForSaleById(id);
            if (open == null)
            {
                return NotFound();
            }

            _open.ChangeStatus(open);


            return Ok("Delete Successfully");
        }

    }
}
