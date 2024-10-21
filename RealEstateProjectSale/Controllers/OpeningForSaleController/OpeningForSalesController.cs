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
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.OpeningForSaleController
{
    [Route("api/open-for-sales")]
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
        [SwaggerOperation(Summary = "Get All OpeningForSale")]
        public IActionResult GetAllOpeningForSale()
        {
            try
            {
                if (_open.GetOpeningForSales() == null)
                {
                    return NotFound(new
                    {
                        message = "OpeningForSale not found."
                    });
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get OpeningForSale By ID")]
        public IActionResult GetOpeningForSaleByID(Guid id)
        {
            var open = _open.GetOpeningForSaleById(id);

            if (open != null)
            {
                var responese = _mapper.Map<OpeningForSaleVM>(open);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "OpeningForSale not found."
            });

        }

        [HttpGet("project/{projectId}")]
        [SwaggerOperation(Summary = "Get OpeningForSale by ProjectID")]
        public IActionResult GetOpeningForSaleByProjectID(Guid projectId)
        {
            var open = _open.GetPropertyByProjectID(projectId);

            if (open != null)
            {
                var responese = open.Select(open => _mapper.Map<OpeningForSaleVM>(open)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "OpeningForSale not found."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "OpeningForSale not found."
            });

        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Search OpeningForSale by Decision Name")]
        public ActionResult<OpeningForSale> SearchOpeningForSaleByName(string decisionName)
        {
            if (_open.GetOpeningForSales() == null)
            {
                return NotFound(new
                {
                    message = "OpeningForSale not found."
                });
            }
            var open = _open.SearchOpeningForSale(decisionName);

            if (open == null)
            {
                return NotFound(new
                {
                    message = "OpeningForSale not found."
                });
            }

            return Ok(open);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new OpeningForSale")]
        public IActionResult AddNew(OpeningForSaleRequestDTO open)
        {
            try
            {
                string startDateInput = open.StartDate;
                string endDateInput = open.EndDate;
                string checkinDateInput = open.CheckinDate;

                DateTime parsedStartDate = DateTime.ParseExact(startDateInput, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime parsedEndDate = DateTime.ParseExact(endDateInput, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime parsedCheckinDate = DateTime.ParseExact(checkinDateInput, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                var newOpen = new OpeningForSaleCreateDTO
                {
                    OpeningForSaleID = Guid.NewGuid(),
                    DecisionName = open.DecisionName,
                    StartDate = parsedStartDate,
                    EndDate = parsedEndDate,
                    CheckinDate = parsedCheckinDate,
                    SaleType = open.SaleType,
                    ReservationPrice = open.ReservationPrice,
                    Description = open.Description,
                    Status = true,
                    ProjectID = open.ProjectID
                };

                var opening = _mapper.Map<OpeningForSale>(newOpen);
                _open.AddNew(opening);

                return Ok(new
                {
                    message = "Create OpeningForSale Successfully"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update OpeningForSale by ID")]
        public IActionResult UpdateComment([FromForm] OpeningForSaleUpdateDTO open, Guid id)
        {
            try
            {
                string startDateInput = open.StartDate;
                string endDateInput = open.EndDate;
                string checkinDateInput = open.CheckinDate;

                DateTime parsedStartDate = DateTime.ParseExact(startDateInput, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime parsedEndDate = DateTime.ParseExact(startDateInput, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime parsedCheckinDate = DateTime.ParseExact(startDateInput, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                var existingOpen = _open.GetOpeningForSaleById(id);
                if (existingOpen != null)
                {
                    if (!string.IsNullOrEmpty(open.DecisionName))
                    {
                        existingOpen.DecisionName = open.DecisionName;
                    }
                    if (!string.IsNullOrEmpty(open.StartDate))
                    {
                        existingOpen.StartDate = parsedStartDate;
                    }
                    if (!string.IsNullOrEmpty(open.EndDate))
                    {
                        existingOpen.EndDate = parsedEndDate;
                    }
                    if (!string.IsNullOrEmpty(open.CheckinDate))
                    {
                        existingOpen.CheckinDate = parsedCheckinDate;
                    }
                    if (!string.IsNullOrEmpty(open.SaleType))
                    {
                        existingOpen.SaleType = open.SaleType;
                    }
                    if (open.ReservationPrice.HasValue)
                    {
                        existingOpen.ReservationPrice = open.ReservationPrice.Value;
                    }
                    if (!string.IsNullOrEmpty(open.Description))
                    {
                        existingOpen.Description = open.Description;
                    }
                    if (open.Status.HasValue)
                    {
                        existingOpen.Status = open.Status.Value;
                    }

                    _open.UpdateOpeningForSale(existingOpen);

                    return Ok(new
                    {
                        message = "OpeningForSale Comment Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "OpeningForSale not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete OpeningForSale by ID")]
        public IActionResult DeleteOpeningForSale(Guid id)
        {
            if (_open.GetOpeningForSaleById(id) == null)
            {
                return NotFound(new
                {
                    message = "OpeningForSale not found."
                });
            }
            var open = _open.GetOpeningForSaleById(id);
            if (open == null)
            {
                return NotFound(new
                {
                    message = "OpeningForSale not found."
                });
            }

            _open.ChangeStatus(open);


            return Ok(new
            {
                message = "Delete OpeningForSale Successfully"
            });
        }

    }
}
