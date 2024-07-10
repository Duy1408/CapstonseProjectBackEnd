using System;
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

namespace RealEstateProjectSale.Controllers.SalespolicyController
{
    [Route("api/[controller]")]
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
        [Route("GetAllSalePolicy")]
        public IActionResult GetAllComment()
        {
            try
            {
                if (_sale.GetSalespolicys()==null)
                {
                    return NotFound();
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

        [HttpGet("GetSalePolicyByID/{id}")]
        public IActionResult GetSalePolicyByID(Guid id)
        {
            var sale = _sale.GetSalespolicyById(id);

            if (sale != null)
            {
                var responese = _mapper.Map<SalepolicyVM>(sale);

                return Ok(responese);
            }

            return NotFound();

        }

        // PUT: api/Salespolicies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("UpdateSalePolicy/{id}")]
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


                    _sale.UpdateSalespolicy(existingSale);

                    return Ok("Update SalePolicy Successfully");

                }

                return NotFound("SalePolicy not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/Salespolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AddNewSalePolicy")]
        public IActionResult AddNew(SalepolicyCreateDTO sale)
        {
            try
            {

                var newSale = new SalepolicyCreateDTO
                {

                    SalesPolicyID = Guid.NewGuid(),
                    SalesPolicyType = sale.SalesPolicyType,
                    ExpressTime = DateTime.Now,
                    PeopleApplied = null,
                    Status = true,
                    ProjectID = sale.ProjectID,
                 
                };

                var salepolicy = _mapper.Map<Salespolicy>(newSale);
               
                _sale.AddNew(salepolicy);

                return Ok("Create SalePolicy Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Salespolicies/5
        [HttpDelete("DeleteSalePolicy/{id}")]
        public IActionResult DeleteSalePolicy(Guid id)
        {
            if (_sale.GetSalespolicys()==null)
            {
                return NotFound();
            }
            var sale = _sale.GetSalespolicyById(id);
            if (sale == null)
            {
                return NotFound();
            }

            _sale.ChangeStatus(sale);


            return Ok("Delete Successfully");
        }



    }
}
