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
        public IActionResult AddNewPromotionDetail(PromotionDetailCreateDTO detail)
        {
            try
            {
                var _detail = _mapper.Map<PromotionDetail>(detail);
                _detailServices.AddNewPromotionDetail(_detail);

                return Ok("Create PromotionDetail Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);


            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePromotionDetail(PromotionDetailUpdateDTO detail, Guid id)
        {
            try
            {
                var existingDetail = _detailServices.GetPromotionDetailByID(id);
                if (existingDetail != null)
                {
                    detail.PromotionDetaiID = existingDetail.PromotionDetaiID;
                    detail.PromotionID = existingDetail.PromotionID;
                    detail.PropertiesTypeID = existingDetail.PropertiesTypeID;

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
