using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Stripe;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.OpenForSaleDetailController
{
    [Route("api/open-for-sale-details")]
    [ApiController]
    public class OpenForSaleDetailController : ControllerBase
    {
        private readonly IOpenForSaleDetailServices _detailServices;
        private readonly IPropertyServices _propertyService;
        private readonly IOpeningForSaleServices _openServices;
        private readonly IMapper _mapper;

        public OpenForSaleDetailController(IOpenForSaleDetailServices detailServices, IMapper mapper,
            IOpeningForSaleServices openServices, IPropertyServices propertyService)
        {
            _detailServices = detailServices;
            _openServices = openServices;
            _mapper = mapper;
            _propertyService = propertyService;
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
                        message = "Chi tiết đợt mở bán không tồn tại."
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
                message = "Chi tiết đợt mở bán không tồn tại."
            });

        }

        [HttpGet("open-for-sale/{openId}")]
        [SwaggerOperation(Summary = "Get OpenForSaleDetail By OpeningForSaleID")]
        public IActionResult GetOpenForSaleDetailByOpeningForSaleID(Guid openId)
        {
            var details = _detailServices.GetOpenForSaleDetailByOpeningForSaleID(openId);

            if (details != null)
            {
                var responese = _mapper.Map<List<OpenForSaleDetailVM>>(details);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chi tiết đợt mở bán không tồn tại."
            });

        }

        [HttpGet("{propertyId}/{openId}")]
        [SwaggerOperation(Summary = "Get OpenForSaleDetail By PropertyID and OpeningForSaleID")]
        public IActionResult GetDetailByPropertyIdOpenId(Guid propertyId, Guid openId)
        {
            var detail = _detailServices.GetDetailByPropertyIdOpenId(propertyId, openId);

            if (detail != null)
            {
                var responese = _mapper.Map<OpenForSaleDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chi tiết đợt mở bán không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new OpenForSaleDetail")]
        public IActionResult AddNewOpenForSaleDetail(OpenForSaleDetailCreateDTO detail)
        {
            try
            {
                var existingProperty = _propertyService.GetPropertyById(detail.PropertyID!.Value);
                if (existingProperty == null)
                {
                    return NotFound(new
                    {
                        message = "Căn không tồn tại."
                    });
                }
                if (existingProperty.Status != PropertyStatus.ChuaBan.GetEnumDescription())
                {
                    return BadRequest(new
                    {
                        message = "Căn này phải có trạng thái Chưa bán."
                    });
                }

                var existingOpen = _openServices.GetOpeningForSaleById(detail.OpeningForSaleID!.Value);
                if (existingOpen == null)
                {
                    return NotFound(new
                    {
                        message = "Chi tiết đợt mở bán không tồn tại."
                    });
                }

                if (existingProperty.ProjectCategoryDetailID != existingOpen.ProjectCategoryDetailID)
                {
                    return BadRequest(new
                    {
                        message = "Căn và đợt mở bán khác loại hình dự án."
                    });
                }

                var newDetail = new OpenForSaleDetailCreateDTO
                {
                    OpeningForSaleID = detail.OpeningForSaleID,
                    PropertyID = detail.PropertyID,
                    Price = detail.Price,
                    Note = detail.Note
                };

                var _detail = _mapper.Map<OpenForSaleDetail>(newDetail);
                _detailServices.AddNewOpenForSaleDetail(_detail);

                var property = _propertyService.GetPropertyById(newDetail.PropertyID!.Value);
                property.Status = PropertyStatus.GiuCho.GetEnumDescription();
                _propertyService.UpdateProperty(property);

                return Ok(new
                {
                    message = "Tạo chi tiết đợt mở bán thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update OpenForSaleDetail by ID")]
        public IActionResult UpdateOpenForSaleDetail([FromForm] OpenForSaleDetailUpdateDTO detail, Guid propertyId, Guid openId)
        {
            try
            {
                var existingDetail = _detailServices.GetDetailByPropertyIdOpenId(propertyId, openId);
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
                    if (detail.PropertyID.HasValue)
                    {
                        existingDetail.PropertyID = detail.PropertyID.Value;
                    }
                    if (detail.OpeningForSaleID.HasValue)
                    {
                        existingDetail.OpeningForSaleID = detail.OpeningForSaleID.Value;
                    }

                    _detailServices.UpdateOpenForSaleDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Cập nhật chi tiết đợt mở bán thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Chi tiết đợt mở bán không tồn tại."
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
                        message = "Xóa chi tiết đợt mở bán thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Chi tiết đợt mở bán không tồn tại."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
