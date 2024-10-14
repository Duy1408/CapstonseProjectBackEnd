using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public ProjectCategoryDetailController(IProjectCategoryDetailServices detailServices, IMapper mapper)
        {
            _detailServices = detailServices;
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
                        message = "ProjectCategoryDetail not found."
                    });
                }
                var details = _detailServices.GetAllProjectCategoryDetail();
                var response = _mapper.Map<List<ProjectCategoryDetailVM>>(details);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{projectID}/{propertyCategoryID}")]
        [SwaggerOperation(Summary = "Get ProjectCategoryDetail By ProjectID and PropertyCategoryID")]
        public IActionResult GetPropertyCategoryByID(Guid projectID, Guid propertyCategoryID)
        {
            var detail = _detailServices.GetProjectCategoryDetailByID(projectID, propertyCategoryID);

            if (detail != null)
            {
                var responese = _mapper.Map<ProjectCategoryDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "ProjectCategoryDetail not found."
            });

        }

        [HttpGet("{projectID}")]
        [SwaggerOperation(Summary = "Get ProjectCategoryDetail By ProjectID")]
        public IActionResult GetProjectCategoryDetailByProjectID(Guid projectID)
        {
            var detail = _detailServices.GetProjectCategoryDetailByProjectID(projectID);

            if (detail != null)
            {
                var responese = _mapper.Map<List<ProjectCategoryDetailVM>>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "ProjectCategoryDetail not found."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new ProjectCategoryDetail")]
        public IActionResult AddNewProjectCategoryDetail(ProjectCategoryDetailCreateDTO detail)
        {
            try
            {

                var newDetail = new ProjectCategoryDetailCreateDTO
                {
                    ProjectID = detail.ProjectID,
                    PropertyCategoryID = detail.PropertyCategoryID,
                };

                var _detail = _mapper.Map<ProjectCategoryDetail>(newDetail);
                _detailServices.AddNewProjectCategoryDetail(_detail);

                return Ok(new
                {
                    message = "Create ProjectCategoryDetail Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{projectID}/{propertyCategoryID}")]
        [SwaggerOperation(Summary = "Update ProjectCategoryDetail By ID")]
        public IActionResult UpdateProjectCategoryDetail([FromForm] ProjectCategoryDetailUpdateDTO detail, Guid projectID, Guid propertyCategoryID)
        {
            try
            {
                var existingDetail = _detailServices.GetProjectCategoryDetailByID(projectID, propertyCategoryID);
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
                        message = "Update ProjectCategoryDetail Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "ProjectCategoryDetail not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{projectID}/{propertyCategoryID}")]
        [SwaggerOperation(Summary = "Delete ProjectCategoryDetail By ProjectID And PropertyCategoryID")]
        public IActionResult DeleteProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID)
        {
            try
            {
                var detail = _detailServices.GetProjectCategoryDetailByProjectID(projectID);
                if (detail != null)
                {
                    _detailServices.DeleteProjectCategoryDetailByID(projectID, propertyCategoryID);
                    return Ok(new
                    {
                        message = "Delete ProjectCategoryDetail Successfully"
                    });
                }

                return NotFound(new
                {
                    message = "ProjectCategoryDetail not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
