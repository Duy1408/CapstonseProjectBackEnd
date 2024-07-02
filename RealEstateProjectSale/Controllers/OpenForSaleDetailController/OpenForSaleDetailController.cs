using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.OpenForSaleDetailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenForSaleDetailController : ControllerBase
    {
        private readonly IOpenForSaleDetailServices _detailServices;
        private readonly IMapper _mapper;

        public OpenForSaleDetailController(IOpenForSaleDetailServices detailServices, IMapper mapper)
        {
            _detailServices = detailServices;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOpenForSaleDetail()
        {
            try
            {
                if (_detailServices.GetAllOpenForSaleDetail() == null)
                {
                    return NotFound();
                }
                var details = _detailServices.GetAllOpenForSaleDetail();
                var response = _mapper.Map<List<OpenForSaleDetailVM>>(details);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOpenForSaleDetailByID(Guid id)
        {
            var detail = _detailServices.GetOpenForSaleDetailByID(id);

            if (detail != null)
            {
                var responese = _mapper.Map<OpenForSaleDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult AddNewOpenForSaleDetail(OpenForSaleDetailCreateDTO detail)
        {
            try
            {
                var _detail = _mapper.Map<OpenForSaleDetail>(detail);
                _detailServices.AddNewOpenForSaleDetail(_detail);

                return Ok("Create Account Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOpenForSaleDetail(OpenForSaleDetailUpdateDTO detail, Guid id)
        {
            try
            {
                var existingDetail = _detailServices.GetOpenForSaleDetailByID(id);
                if (existingDetail != null)
                {
                    detail.OpenForSaleDetailID = existingDetail.OpenForSaleDetailID;
                    detail.OpeningForSaleID = existingDetail.OpeningForSaleID;
                    detail.PropertiesID = existingDetail.PropertiesID;

                    var _detail = _mapper.Map<OpenForSaleDetail>(detail);
                    _detailServices.UpdateOpenForSaleDetail(_detail);

                    return Ok("Update Successfully");

                }

                return NotFound("OpenForSaleDetail not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOpenForSaleDetail(Guid id)
        {
            try
            {
                var detail = _detailServices.GetOpenForSaleDetailByID(id);
                if (detail != null)
                {
                    _detailServices.DeleteOpenForSaleDetailByID(id);
                    return Ok();
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
