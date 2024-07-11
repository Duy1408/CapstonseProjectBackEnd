using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.PromotionDetailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionDetailController : ControllerBase
    {
        private readonly IPromotionDetailServices _detailServices;
        private readonly IMapper _mapper;

        public PromotionDetailController(IPromotionDetailServices detailServices, IMapper mapper)
        {
            _detailServices = detailServices;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllPromotionDetail()
        {
            try
            {
                if (_detailServices.GetAllPromotionDetail() == null)
                {
                    return NotFound();
                }
                var details = _detailServices.GetAllPromotionDetail();
                var response = _mapper.Map<List<PromotionDetailVM>>(details);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetPromotionDetailByID(Guid id)
        {
            var detail = _detailServices.GetPromotionDetailByID(id);

            if (detail != null)
            {
                var responese = _mapper.Map<PromotionDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound();

        }
        [HttpPost]
        [Route("AddNewPromotionDetail")]
        public IActionResult AddNew(PromotionDetailCreateDTO pro)
        {
            try
            {

                var newPro = new PromotionDetailCreateDTO
                {
                   

                    PromotionDetaiID = Guid.NewGuid(),
                    Description = pro.Description,
                    PromotionType = pro.PromotionType,
                    DiscountPercent = pro.DiscountPercent,
                    DiscountAmount = pro.DiscountAmount,
                    Amount = pro.Amount,
                    PromotionID = pro.PromotionID,
                    PropertiesTypeID = pro.PropertiesTypeID,


                };

                var promotiondetail = _mapper.Map<PromotionDetail>(newPro);

                _detailServices.AddNewPromotionDetail(promotiondetail);

                return Ok("Create Promotiondetail Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePromotionDetail([FromForm] PromotionDetailUpdateDTO detail, Guid id)
        {
            try
            {
                var existingDetail = _detailServices.GetPromotionDetailByID(id);
                if (existingDetail != null)
                {
                    if (!string.IsNullOrEmpty(detail.Description))
                    {
                        existingDetail.Description = detail.Description;
                    }
                    if (!string.IsNullOrEmpty(detail.PromotionType))
                    {
                        existingDetail.PromotionType = detail.PromotionType;
                    }
                    if (detail.DiscountPercent.HasValue)
                    {
                        existingDetail.DiscountPercent = detail.DiscountPercent.Value;
                    }
                    if (detail.DiscountAmount.HasValue)
                    {
                        existingDetail.DiscountAmount = detail.DiscountAmount.Value;
                    }
                    if (detail.Amount.HasValue)
                    {
                        existingDetail.Amount = detail.Amount.Value;
                    }


              

                    var _detail = _mapper.Map<PromotionDetail>(detail);
                    _detailServices.UpdatePromotionDetail(_detail);

                    return Ok("Update Successfully");

                }

                return NotFound("PromotionDetail not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePromotionDetail(Guid id)
        {
            try
            {
                var detail = _detailServices.GetPromotionDetailByID(id);
                if (detail != null)
                {
                    _detailServices.DeletePromotionDetailByID(id);
                    return Ok("Delete PromotionDetail Successfully");
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
