using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.OpenForSaleDetailController
{
    [Route("api/open-for-sale-details")]
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
        [SwaggerOperation(Summary = "Get All OpenForSaleDetail")]
        public IActionResult GetAllOpenForSaleDetail()
        {
            try
            {
                if (_detailServices.GetAllOpenForSaleDetail() == null)
                {
                    return NotFound(new
                    {
                        message = "OpenForSaleDetail not found."
                    });
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
        [SwaggerOperation(Summary = "Get OpenForSaleDetail By ID")]
        public IActionResult GetOpenForSaleDetailByID(Guid id)
        {
            var detail = _detailServices.GetOpenForSaleDetailByID(id);

            if (detail != null)
            {
                var responese = _mapper.Map<OpenForSaleDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "OpenForSaleDetail not found."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new OpenForSaleDetail")]
        public IActionResult AddNewOpenForSaleDetail(OpenForSaleDetailCreateDTO detail)
        {
            try
            {
                var newDetail = new OpenForSaleDetailCreateDTO
                {
                    OpeningForSaleID = detail.OpeningForSaleID,
                    PropertyID = detail.PropertyID,
                    Price = detail.Price,
                    Note = detail.Note
                };

                var _detail = _mapper.Map<OpenForSaleDetail>(newDetail);
                _detailServices.AddNewOpenForSaleDetail(_detail);

                return Ok(new
                {
                    message = "Create OpenForSaleDetail Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update OpenForSaleDetail by ID")]
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
                    if (!string.IsNullOrEmpty(detail.Note))
                    {
                        existingDetail.Note = detail.Note;
                    }

                    _detailServices.UpdateOpenForSaleDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Create OpenForSaleDetail Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "OpenForSaleDetail not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete OpenForSaleDetail by ID")]
        public IActionResult DeleteOpenForSaleDetail(Guid id)
        {
            try
            {
                var detail = _detailServices.GetOpenForSaleDetailByID(id);
                if (detail != null)
                {
                    _detailServices.DeleteOpenForSaleDetailByID(id);
                    return Ok(new
                    {
                        message = "Delete OpenForSaleDetail Successfully"
                    });
                }

                return NotFound(new
                {
                    message = "OpenForSaleDetail not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
