using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSale.Helpers;
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
        private readonly IProjectServices _projectService;
        private readonly IProjectCategoryDetailServices _detailService;
        private readonly IMapper _mapper;

        public OpeningForSalesController(IOpeningForSaleServices open, IMapper mapper,
            IProjectServices projectService, IProjectCategoryDetailServices detailService)
        {
            _open = open;
            _projectService = projectService;
            _mapper = mapper;
            _detailService = detailService;
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

        [HttpGet("categoryDetail/{detailId}")]
        [SwaggerOperation(Summary = "Get OpeningForSale by ProjectCategoryDetailID")]
        public IActionResult GetOpeningForSaleByProjectCategoryDetailID(Guid detailId)
        {
            var open = _open.GetOpeningForSaleByProjectCategoryDetailID(detailId);

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

            if (open == null || open.Count() == 0)
            {
                return NotFound(new
                {
                    message = "OpeningForSale not found."
                });
            }

            var responese = open.Select(open => _mapper.Map<OpeningForSaleVM>(open)).ToList();

            return Ok(responese);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new OpeningForSale")]
        public IActionResult AddNew(OpeningForSaleRequestDTO open)
        {
            try
            {
                var detailID = open.ProjectCategoryDetailID.GetValueOrDefault(Guid.Empty);

                if (detailID == Guid.Empty)
                {
                    throw new ArgumentException("ProjectCategoryDetailID is required.");
                }

                var existingDetail = _detailService.GetProjectCategoryDetailByID(detailID);
                if (existingDetail == null)
                {
                    return NotFound(new
                    {
                        message = "ProjectCategoryDetail not found."
                    });
                }

                //var existingProject = _open.FindByProjectIdAndStatus(existingDetail.ProjectID);
                //if (existingProject != null)
                //{
                //    return NotFound(new
                //    {
                //        message = "An OpeningForSale with the same Project already exists."
                //    });
                //}


                var existingOpen = _open.FindByDetailIdAndStatus(detailID);
                if (existingOpen != null)
                {
                    return BadRequest(new
                    {
                        message = "An OpeningForSale with the same ProjectCategoryDetail already exists."
                    });
                }

                DateTime parsedStartDate = DateTimeHelper.ConvertToDateTime(open.StartDate);
                DateTime parsedEndDate = DateTimeHelper.ConvertToDateTime(open.EndDate);
                DateTime parsedCheckinDate = DateTimeHelper.ConvertToDateTime(open.CheckinDate);

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
                    ProjectCategoryDetailID = detailID
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
                DateTime? parsedStartDate = !string.IsNullOrEmpty(open.StartDate)
                                            ? DateTimeHelper.ConvertToDateTime(open.StartDate)
                                            : (DateTime?)null;

                DateTime? parsedEndDate = !string.IsNullOrEmpty(open.EndDate)
                                            ? DateTimeHelper.ConvertToDateTime(open.EndDate)
                                            : (DateTime?)null;

                DateTime? parsedCheckinDate = !string.IsNullOrEmpty(open.CheckinDate)
                                              ? DateTimeHelper.ConvertToDateTime(open.CheckinDate)
                                                : (DateTime?)null;

                var existingOpen = _open.GetOpeningForSaleById(id);
                if (existingOpen != null)
                {
                    if (!string.IsNullOrEmpty(open.DecisionName))
                    {
                        existingOpen.DecisionName = open.DecisionName;
                    }
                    if (!string.IsNullOrEmpty(open.StartDate))
                    {
                        existingOpen.StartDate = parsedStartDate.Value;
                    }
                    if (!string.IsNullOrEmpty(open.EndDate))
                    {
                        existingOpen.EndDate = parsedEndDate.Value;
                    }
                    if (!string.IsNullOrEmpty(open.CheckinDate))
                    {
                        existingOpen.CheckinDate = parsedCheckinDate.Value;
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
                        message = "Update OpeningForSale Successfully"
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
