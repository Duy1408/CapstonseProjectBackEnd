﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PromotionDetailController
{
    [Route("api/promotion-details")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get All PromotionDetail")]
        public IActionResult GetAllPromotionDetail()
        {
            try
            {
                if (_detailServices.GetAllPromotionDetail() == null)
                {
                    return NotFound(new
                    {
                        message = "Chi tiết gói khuyến mãi không tồn tại."
                    });
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

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get PromotionDetail by ID")]
        public IActionResult GetPromotionDetailByID(Guid id)
        {
            var detail = _detailServices.GetPromotionDetailByID(id);

            if (detail != null)
            {
                var responese = _mapper.Map<PromotionDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chi tiết gói khuyến mãi không tồn tại."
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("promotion/{promotionId}")]
        [SwaggerOperation(Summary = "Get PromotionDetail By PromotionID")]
        public IActionResult GetPromotionDetailByPromotionID(Guid promotionId)
        {
            var details = _detailServices.GetPromotionDetailByPromotionID(promotionId);

            if (details != null)
            {
                var responese = _mapper.Map<List<PromotionDetailVM>>(details);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chi tiết gói khuyến mãi không tồn tại."
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PromotionDetail")]
        public IActionResult AddNew(PromotionDetailCreateDTO pro)
        {
            try
            {
                var detail = _detailServices.GetDetailByPromotionIDPropertyTypeID(pro.PromotionID, pro.PropertyTypeID);
                if (detail != null)
                {
                    return BadRequest(new
                    {
                        message = "Loại căn hộ này đã có khuyến mãi chi tiết."
                    });
                }

                var newPro = new PromotionDetailCreateDTO
                {
                    PromotionDetailID = Guid.NewGuid(),
                    Description = pro.Description,
                    Amount = pro.Amount,
                    PromotionID = pro.PromotionID,
                    PropertyTypeID = pro.PropertyTypeID,
                };

                var promotiondetail = _mapper.Map<PromotionDetail>(newPro);

                _detailServices.AddNewPromotionDetail(promotiondetail);

                return Ok(new
                {
                    message = "Tạo chi tiết gói khuyến mãi thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update PromotionDetail by ID")]
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
                    if (detail.Amount.HasValue)
                    {
                        existingDetail.Amount = detail.Amount.Value;
                    }
                    if (detail.PromotionID.HasValue)
                    {
                        existingDetail.PromotionID = detail.PromotionID.Value;
                    }
                    if (detail.PropertyTypeID.HasValue)
                    {
                        existingDetail.PropertyTypeID = detail.PropertyTypeID.Value;
                    }


                    _detailServices.UpdatePromotionDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Cập nhật chi tiết gói khuyến mãi thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Chi tiết gói khuyến mãi không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete PromotionDetail by ID")]
        public IActionResult DeletePromotionDetail(Guid id)
        {
            try
            {
                var detail = _detailServices.GetPromotionDetailByID(id);
                if (detail != null)
                {
                    _detailServices.DeletePromotionDetailByID(id);
                    return Ok(new
                    {
                        message = "Xóa chi tiết gói khuyến mãi thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Chi tiết gói khuyến mãi không tồn tại."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
