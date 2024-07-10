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

namespace RealEstateProjectSale.Controllers.PromotionController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionServices _pro;
        private readonly IMapper _mapper;


        public PromotionsController(IPromotionServices pro, IMapper mapper)
        {
            _pro = pro;
            _mapper = mapper;
        }

        // GET: api/Promotions
        [HttpGet]
        [Route("GetAllPromotion")]
        public IActionResult GetAllPromotion()
        {
            try
            {
                if (_pro.GetPromotions() == null)
                {
                    return NotFound();
                }
                var pros = _pro.GetPromotions();
                var response = _mapper.Map<List<PromotionVM>>(pros);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Promotions/5

        [HttpGet("GetPromotionByID/{id}")]
        public IActionResult GetPromotionByID(Guid id)
        {
            var pro = _pro.GetPromotionById(id);

            if (pro != null)
            {
                var responese = _mapper.Map<PromotionVM>(pro);

                return Ok(responese);
            }

            return NotFound();

        }

        // PUT: api/Promotions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdatePromotion/{id}")]
        public IActionResult UpdatePromotiont([FromForm] PromotionUpdateDTO pro, Guid id)
        {
            try
            {
                var existingPro = _pro.GetPromotionById(id);
                if (existingPro != null)
                {

                    if (!string.IsNullOrEmpty(pro.PromotionName))
                    {
                        existingPro.PromotionName = pro.PromotionName;
                    }
                    if (!string.IsNullOrEmpty(pro.Description))
                    {
                        existingPro.Description = pro.Description;
                    }
                    if (pro.StartDate.HasValue)
                    {
                        existingPro.StartDate = pro.StartDate.Value;
                    }
                    if (pro.EndDate.HasValue)
                    {
                        existingPro.EndDate = pro.EndDate.Value;
                    }
                    if (pro.Status.HasValue)
                    {
                        existingPro.Status = pro.Status.Value;
                    }
                    _pro.UpdatePromotion(existingPro);


                    return Ok("Update Promotion Successfully");

                }

                return NotFound("Promotion not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Promotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AddNewPromotion")]
        public IActionResult AddNew(PromotionCreateDTO pro)
        {
            try
            {

                var newPro = new PromotionCreateDTO
                {
                    PromotionID = Guid.NewGuid(),
                    PromotionName = pro.PromotionName,
                    Description = pro.Description,
                    StartDate = pro.StartDate,
                    EndDate = pro.EndDate,
                    Status = true,
                    SalesPolicyID = pro.SalesPolicyID

                };

                var promotion = _mapper.Map<Promotion>(newPro);

                _pro.AddNew(promotion);

                return Ok("Create Promotion Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Promotions/5
        [HttpDelete("DeletePromotion/{id}")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            if (_pro.GetPromotions() == null)
            {
                return NotFound();
            }
            var promotion = _pro.GetPromotionById(id);
            if (promotion == null)
            {
                return NotFound();
            }

            _pro.ChangeStatus(promotion);

            return Ok("Delete Successfully");
        }


    }
}
