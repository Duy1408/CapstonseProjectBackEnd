using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.SalespolicyController
{
    [Route("api/sales-policys")]
    [ApiController]
    public class SalespoliciesController : ControllerBase
    {
        private readonly ISalespolicyServices _sale;
        private readonly IMapper _mapper;

        public SalespoliciesController(ISalespolicyServices sale, IMapper mapper)
        {
            _sale = sale;
            _mapper = mapper;
        }

        // GET: api/Salespolicies
        [HttpGet]
        [SwaggerOperation(Summary = "Get All SalePolicy")]
        public IActionResult GetAllSalePolicy()
        {
            try
            {
                if (_sale.GetSalespolicys() == null)
                {
                    return NotFound(new
                    {
                        message = "SalePolicy not found."
                    });
                }
                var sales = _sale.GetSalespolicys();
                var response = _mapper.Map<List<SalepolicyVM>>(sales);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Salespolicies/5

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get SalePolicy by ID")]
        public IActionResult GetSalePolicyByID(Guid id)
        {
            var sale = _sale.GetSalespolicyById(id);

            if (sale != null)
            {
                var responese = _mapper.Map<SalepolicyVM>(sale);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "SalePolicy not found."
            });

        }

        [HttpGet(" project/{projectid}")]
        [SwaggerOperation(Summary = "Get SalePolicy by project ID")]
        public IActionResult GetSalePolicyByProjectID(Guid projectid)
        {
            var sale = _sale.GetSalespolicyByProjectID(projectid);

            if (sale != null)
            {
                var responese = _mapper.Map<SalepolicyVM>(sale);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "SalePolicy not found."
            });

        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update SalePolicy by ID")]
        public IActionResult UpdateSalePolicy([FromForm] SalePolicyUpdateDTO sale, Guid id)
        {
            try
            {
                var existingSale = _sale.GetSalespolicyById(id);
                if (existingSale != null)
                {

                    if (!string.IsNullOrEmpty(sale.SalesPolicyType))
                    {
                        existingSale.SalesPolicyType = sale.SalesPolicyType;
                    }
                    if (!string.IsNullOrEmpty(sale.PeopleApplied))
                    {
                        existingSale.PeopleApplied = sale.PeopleApplied;
                    }
                    if (sale.Status.HasValue)
                    {
                        existingSale.Status = sale.Status.Value;
                    }
                    if (sale.ExpressTime.HasValue)
                    {
                        existingSale.ExpressTime = sale.ExpressTime.Value;
                    }
                    if (sale.ProjectID.HasValue)
                    {
                        existingSale.ProjectID = sale.ProjectID.Value;
                    }


                    _sale.UpdateSalespolicy(existingSale);

                    return Ok(new
                    {
                        message = "Update SalePolicy Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "SalePolicy not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new SalePolicy")]
        public IActionResult AddNew(SalepolicyCreateDTO sale)
        {
            try
            {

                var newSale = new SalepolicyCreateDTO
                {
                    SalesPolicyID = Guid.NewGuid(),
                    SalesPolicyType = sale.SalesPolicyType,
                    ExpressTime = DateTime.Now.Date,
                    PeopleApplied = null,
                    Status = true,
                    ProjectID = sale.ProjectID,
                };

                var salepolicy = _mapper.Map<Salespolicy>(newSale);

                _sale.AddNew(salepolicy);

                return Ok(new
                {
                    message = "Create SalePolicy Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete SalePolicy by ID")]
        public IActionResult DeleteSalePolicy(Guid id)
        {
            if (_sale.GetSalespolicys() == null)
            {
                return NotFound(new
                {
                    message = "SalePolicy not found."
                });
            }
            var sale = _sale.GetSalespolicyById(id);
            if (sale == null)
            {
                return NotFound(new
                {
                    message = "SalePolicy not found."
                });
            }

            _sale.ChangeStatus(sale);


            return Ok(new
            {
                message = "Delete Comment Successfully"
            });
        }



    }
}
