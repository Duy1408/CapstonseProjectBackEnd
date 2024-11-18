using AutoMapper;
using Google.Api.Gax.ResourceNames;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.ProjectCategoryDetailController
{
    [Route("api/project-category-details")]
    [ApiController]
    public class ProjectCategoryDetailController : ControllerBase
    {
        private readonly IProjectCategoryDetailServices _detailServices;
        private readonly IBookingServices _bookServices;
        private readonly IOpeningForSaleServices _openService;
        private readonly IPropertyServices _propertyService;
        private readonly IMapper _mapper;

        public ProjectCategoryDetailController(IProjectCategoryDetailServices detailServices,
            IOpeningForSaleServices openService, IBookingServices bookServices, IMapper mapper,
            IPropertyServices propertyService)
        {
            _detailServices = detailServices;
            _bookServices = bookServices;
            _openService = openService;
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All ProjectCategoryDetail")]
        public IActionResult GetAllProjectCategoryDetail()
        {
            try
            {
                if (_detailServices.GetAllProjectCategoryDetail() == null)
                {
                    return NotFound(new
                    {
                        message = "Loại hình dự án không tồn tại."
                    });
                }
                var detail = _detailServices.GetAllProjectCategoryDetail();
                var details = _mapper.Map<List<ProjectCategoryDetailVM>>(detail);

                var responese = details.Select(detailOpen => new ProjectCategoryDetailOpenVM
                {
                    ProjectCategoryDetailID = detailOpen.ProjectCategoryDetailID,
                    ProjectID = detailOpen.ProjectID,
                    ProjectName = detailOpen.ProjectName,
                    PropertyCategoryID = detailOpen.PropertyCategoryID,
                    PropertyCategoryName = detailOpen.PropertyCategoryName,
                    OpenForSale = _openService.GetOpenForSaleStatusByProjectCategoryDetailID(detailOpen.ProjectCategoryDetailID)
                }).ToList();

                return Ok(responese);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get ProjectCategoryDetail By ID")]
        public IActionResult GetProjectCategoryDetailByID(Guid id)
        {
            var detail = _detailServices.GetProjectCategoryDetailByID(id);

            if (detail != null)
            {
                var responese = _mapper.Map<ProjectCategoryDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Loại hình dự án không tồn tại."
            });

        }

        [HttpGet("property")]
        [SwaggerOperation(Summary = "Get ProjectCategoryDetail By ZoneID And UnitTypeID")]
        public IActionResult GetProjectCategoryDetailByZoneIDUnitTypeID(Guid id)
        {
            var detail = _detailServices.GetProjectCategoryDetailByID(id);

            if (detail != null)
            {
                var responese = _mapper.Map<ProjectCategoryDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Loại hình dự án không tồn tại."
            });

        }

        [HttpGet("{projectID}/{propertyCategoryID}")]
        [SwaggerOperation(Summary = "Get ProjectCategoryDetail By ProjectID and PropertyCategoryID")]
        public IActionResult GetPropertyCategoryByID(Guid projectID, Guid propertyCategoryID)
        {
            var detail = _detailServices.GetDetailByProjectIDCategoryID(projectID, propertyCategoryID);

            if (detail != null)
            {
                var responese = _mapper.Map<ProjectCategoryDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Loại hình dự án không tồn tại."
            });

        }

        [HttpGet("project/{projectID}")]
        [SwaggerOperation(Summary = "Get ProjectCategoryDetail By ProjectID")]
        public IActionResult GetProjectCategoryDetailByProjectID(Guid projectID)
        {
            var detail = _detailServices.GetProjectCategoryDetailByProjectID(projectID);

            if (detail != null)
            {


                var details = _mapper.Map<List<ProjectCategoryDetailVM>>(detail);

                var responese = details.Select(detailOpen => new ProjectCategoryDetailOpenVM
                {
                    ProjectCategoryDetailID = detailOpen.ProjectCategoryDetailID,
                    ProjectID = detailOpen.ProjectID,
                    ProjectName = detailOpen.ProjectName,
                    PropertyCategoryID = detailOpen.PropertyCategoryID,
                    PropertyCategoryName = detailOpen.PropertyCategoryName,
                    OpenForSale = _openService.GetOpenForSaleStatusByProjectCategoryDetailID(detailOpen.ProjectCategoryDetailID)
                }).ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Loại hình dự án không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new ProjectCategoryDetail")]
        public IActionResult AddNewProjectCategoryDetail(ProjectCategoryDetailCreateDTO detail)
        {
            try
            {
                var categoryDetail = _detailServices.GetDetailByProjectIDCategoryID(detail.ProjectID, detail.PropertyCategoryID);
                if (categoryDetail != null)
                {
                    return BadRequest(new
                    {
                        message = "Dự án đã có loại căn hộ."
                    });
                }

                var newDetail = new ProjectCategoryDetailCreateDTO
                {
                    ProjectCategoryDetailID = Guid.NewGuid(),
                    ProjectID = detail.ProjectID,
                    PropertyCategoryID = detail.PropertyCategoryID,
                };

                var _detail = _mapper.Map<ProjectCategoryDetail>(newDetail);
                _detailServices.AddNewProjectCategoryDetail(_detail);

                return Ok(new
                {
                    message = "Tạo loại hình dự án thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update ProjectCategoryDetail By ID")]
        public IActionResult UpdateProjectCategoryDetail([FromForm] ProjectCategoryDetailUpdateDTO detail, Guid id)
        {
            try
            {
                var existingDetail = _detailServices.GetProjectCategoryDetailByID(id);
                if (existingDetail != null)
                {
                    if (detail.ProjectID.HasValue)
                    {
                        existingDetail.ProjectID = detail.ProjectID.Value;
                    }
                    if (detail.PropertyCategoryID.HasValue)
                    {
                        existingDetail.PropertyCategoryID = detail.PropertyCategoryID.Value;
                    }


                    _detailServices.UpdateProjectCategoryDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Cập nhật loại hình dự án thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Loại hình dự án không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete ProjectCategoryDetail By ID")]
        public IActionResult DeleteProjectCategoryDetailByID(Guid id)
        {
            try
            {
                var detail = _detailServices.GetProjectCategoryDetailByID(id);
                if (detail != null)
                {
                    var booking = _bookServices.GetBookingByCategoryDetailID(id);
                    if (booking != null && booking.Any())
                    {
                        return BadRequest(new
                        {
                            message = "Booking đã có loại hình dự án này."
                        });
                    }

                    var openForSale = _openService.GetOpeningForSaleByProjectCategoryDetailID(id);
                    if (openForSale != null && openForSale.Any())
                    {
                        return BadRequest(new
                        {
                            message = "Đợt mở bán đã có loại hình dự án này."
                        });
                    }

                    var property = _propertyService.GetPropertyByCategoryDetailID(id);
                    if (property != null && property.Any())
                    {
                        return BadRequest(new
                        {
                            message = "Căn hộ đã có loại hình dự án này."
                        });
                    }

                    _detailServices.DeleteProjectCategoryDetailByID(id);
                    return Ok(new
                    {
                        message = "Xóa loại hình dự án thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Loại hình dự án không tồn tại."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
