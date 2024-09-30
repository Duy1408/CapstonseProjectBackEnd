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
        [Route("GetAllOpenForSaleDetail")]
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

        [HttpGet("GetOpenForSaleDetailByID/{id}")]
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
        [Route("AddNewOpenForSaleDetail")]
        public IActionResult AddNewOpenForSaleDetail(OpenForSaleDetailCreateDTO detail)
        {
            try
            {
                var newDetail = new OpenForSaleDetailCreateDTO
                {
                    OpenForSaleDetailID = Guid.NewGuid(),
                    Price = detail.Price,
                    Discount = detail.Discount,
                    Note = detail.Note,
                    OpeningForSaleID = detail.OpeningForSaleID,
                    PropertyID = detail.PropertyID
                };

                var _detail = _mapper.Map<OpenForSaleDetail>(newDetail);
                _detailServices.AddNewOpenForSaleDetail(_detail);

                return Ok("Create OpenForSaleDetail Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateOpenForSaleDetail/{id}")]
        public IActionResult UpdateOpenForSaleDetail([FromForm] OpenForSaleDetailUpdateDTO detail, Guid id)
        {
            try
            {
                var existingDetail = _detailServices.GetOpenForSaleDetailByID(id);
                if (existingDetail != null)
                {
                    if (detail.Price.HasValue)
                    {
                        existingDetail.Price = detail.Price.Value;
                    }
                    //bug
                    //if (detail.Discount.HasValue)
                    //{
                    //    existingDetail.Discount = detail.Discount.Value;
                    //}
                    if (!string.IsNullOrEmpty(detail.Note))
                    {
                        existingDetail.Note = detail.Note;
                    }

                    _detailServices.UpdateOpenForSaleDetail(existingDetail);

                    return Ok("Update OpenForSaleDetail Successfully");

                }

                return NotFound("OpenForSaleDetail not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOpenForSaleDetail/{id}")]
        public IActionResult DeleteOpenForSaleDetail(Guid id)
        {
            try
            {
                var detail = _detailServices.GetOpenForSaleDetailByID(id);
                if (detail != null)
                {
                    _detailServices.DeleteOpenForSaleDetailByID(id);
                    return Ok("Deleted OpenForSaleDetail Successfully");
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
