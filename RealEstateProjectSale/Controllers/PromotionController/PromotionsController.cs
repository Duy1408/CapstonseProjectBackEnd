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
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PromotionController
{
    [Route("api/promotions")]
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
        [SwaggerOperation(Summary = "Get All Promotion")]
        public IActionResult GetAllPromotion()
        {
            try
            {
                if (_pro.GetPromotions() == null)
                {
                    return NotFound(new
                    {
                        message = "Promotion not found."
                    });
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Promotion by ID")]
        public IActionResult GetPromotionByID(Guid id)
        {
            var pro = _pro.GetPromotionById(id);

            if (pro != null)
            {
                var responese = _mapper.Map<PromotionVM>(pro);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Promotion not found."
            });

        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Promotion by ID")]
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
              
                    if (pro.Status.HasValue)
                    {
                        existingPro.Status = pro.Status.Value;
                    }
                    if (pro.SalesPolicyID.HasValue)
                    {
                        existingPro.SalesPolicyID = pro.SalesPolicyID.Value;
                    }
                    _pro.UpdatePromotion(existingPro);


                    return Ok(new
                    {
                        message = "Update Promotion Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Promotion not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Promotion")]
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

                return Ok(new
                {
                    message = "Create Promotion Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Promotion by ID")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            if (_pro.GetPromotions() == null)
            {
                return NotFound(new
                {
                    message = "Promotion not found."
                });
            }
            var promotion = _pro.GetPromotionById(id);
            if (promotion == null)
            {
                return NotFound(new
                {
                    message = "Promotion not found."
                });
            }

            _pro.ChangeStatus(promotion);

            return Ok(new
            {
                message = "Delete Promotion Successfully"
            });
        }


    }
}
