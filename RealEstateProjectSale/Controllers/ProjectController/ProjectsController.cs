using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectServices _project;
        private readonly IPagingServices _pagingServices;
        private readonly IMapper _mapper;
        private readonly IFileUploadToBlobService _fileService;

        public static int PAGE_SIZE { get; set; } = 5;


        public ProjectsController(IProjectServices project, IPagingServices pagingServices,
                    IFileUploadToBlobService fileService, IMapper mapper)
        {
            _project = project;
            _pagingServices = pagingServices;
            _mapper = mapper;
            _fileService = fileService;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Get All Project")]
        public IActionResult GetAllProject([FromQuery] string? projectName, [FromQuery] int page = 1)
        {
            try
            {
                var projects = _project.GetProjects();

                if (projects == null || !projects.Any())
                {
                    return NotFound(new
                    {
                        message = "Project not found."
                    });
                }

                var projectsQuery = string.IsNullOrEmpty(projectName)
                            ? _project.GetProjects().AsQueryable()
                            : _project.SearchProject(projectName);

                var pagedProjects = _pagingServices.GetPagedList(projectsQuery, page, PAGE_SIZE);

                if (pagedProjects == null || !pagedProjects.Any())
                {
                    return NotFound(new
                    {
                        message = "Project not found."
                    });
                }

                var response = pagedProjects.Select(project => new ProjectVM
                {
                    ProjectID = project.ProjectID,
                    ProjectName = project.ProjectName,
                    Location = project.Location,
                    Investor = project.Investor,
                    GeneralContractor = project.GeneralContractor,
                    DesignUnit = project.DesignUnit,
                    TotalArea = project.TotalArea,
                    Scale = project.Scale,
                    BuildingDensity = project.BuildingDensity,
                    TotalNumberOfApartment = project.TotalNumberOfApartment,
                    LegalStatus = project.LegalStatus,
                    HandOver = project.HandOver,
                    Convenience = project.Convenience,
                    Images = project.Image?.Split(',').ToList() ?? new List<string>(),
                    Status = project.Status,
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "GetProjectByID")]
        public IActionResult GetProjectByID(Guid id)
        {
            try
            {
                var project = _project.GetProjectById(id);

                if (project == null)
                {
                    return NotFound(new
                    {
                        message = "Project not found."
                    });
                }

                var response = new ProjectVM
                {
                    ProjectID = project.ProjectID,
                    ProjectName = project.ProjectName,
                    Location = project.Location,
                    Investor = project.Investor,
                    GeneralContractor = project.GeneralContractor,
                    DesignUnit = project.DesignUnit,
                    TotalArea = project.TotalArea,
                    Scale = project.Scale,
                    BuildingDensity = project.BuildingDensity,
                    TotalNumberOfApartment = project.TotalNumberOfApartment,
                    LegalStatus = project.LegalStatus,
                    HandOver = project.HandOver,
                    Convenience = project.Convenience,
                    Images = project.Image?.Split(',').ToList() ?? new List<string>(),
                    Status = project.Status,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "UpdateProject")]
        public IActionResult UpdateProject([FromForm] ProjectUpdateDTO project, Guid id)
        {
            try
            {
                var imageUrls = _fileService.UploadMultipleImages(project.Images.ToList(), "projectimage");

                var existingProject = _project.GetProjectById(id);
                if (existingProject != null)
                {

                    if (!string.IsNullOrEmpty(project.ProjectName))
                    {
                        existingProject.ProjectName = project.ProjectName;
                    }
                    if (!string.IsNullOrEmpty(project.Location))
                    {
                        existingProject.Location = project.Location;
                    }
                    if (!string.IsNullOrEmpty(project.Investor))
                    {
                        existingProject.Investor = project.Investor;
                    }
                    if (!string.IsNullOrEmpty(project.GeneralContractor))
                    {
                        existingProject.GeneralContractor = project.GeneralContractor;
                    }
                    if (!string.IsNullOrEmpty(project.DesignUnit))
                    {
                        existingProject.DesignUnit = project.DesignUnit;
                    }
                    if (!string.IsNullOrEmpty(project.TotalArea))
                    {
                        existingProject.TotalArea = project.TotalArea;
                    }
                    if (!string.IsNullOrEmpty(project.Scale))
                    {
                        existingProject.Scale = project.Scale;
                    }
                    if (!string.IsNullOrEmpty(project.BuildingDensity))
                    {
                        existingProject.BuildingDensity = project.BuildingDensity;
                    }
                    if (!string.IsNullOrEmpty(project.TotalNumberOfApartment))
                    {
                        existingProject.TotalNumberOfApartment = project.TotalNumberOfApartment;
                    }
                    if (!string.IsNullOrEmpty(project.LegalStatus))
                    {
                        existingProject.LegalStatus = project.LegalStatus;
                    }
                    if (!string.IsNullOrEmpty(project.HandOver))
                    {
                        existingProject.HandOver = project.HandOver;
                    }
                    if (!string.IsNullOrEmpty(project.Convenience))
                    {
                        existingProject.Convenience = project.Convenience;
                    }

                    if (imageUrls.Count > 0)
                    {
                        existingProject.Image = string.Join(",", imageUrls);
                    }
                    if (!string.IsNullOrEmpty(project.Status) && int.TryParse(project.Status, out int statusValue))
                    {
                        if (Enum.IsDefined(typeof(ProjectStatus), statusValue))
                        {
                            var statusEnum = (ProjectStatus)statusValue;
                            var statusDescription = statusEnum.GetEnumDescription();
                            existingProject.Status = statusDescription;
                        }
                    }



                    _project.UpdateProject(existingProject);

                    return Ok(new
                    {
                        message = "Update Project Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Project not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "AddNewProject")]
        public IActionResult AddNew([FromForm] ProjectRequestDTO pro)
        {
            try
            {
                var imageUrls = _fileService.UploadMultipleImages(pro.Images.ToList(), "projectimage");

                var newPro = new ProjectCreateDTO
                {
                    ProjectID = Guid.NewGuid(),
                    ProjectName = pro.ProjectName,
                    Location = pro.Location,
                    Investor = pro.Investor,
                    GeneralContractor = pro.GeneralContractor,
                    DesignUnit = pro.DesignUnit,
                    TotalArea = pro.TotalArea,
                    Scale = pro.Scale,
                    BuildingDensity = pro.BuildingDensity,
                    TotalNumberOfApartment = pro.TotalNumberOfApartment,
                    LegalStatus = pro.LegalStatus,
                    HandOver = pro.HandOver,
                    Convenience = pro.Convenience,
                    Status = ProjectStatus.SapMoBan.GetEnumDescription(),
                    Images = pro.Images.Count > 0 ? pro.Images.First() : null, // Store first image for reference
                };


                var project = _mapper.Map<Project>(newPro);

                project.Image = string.Join(",", imageUrls); // Store all image URLs as a comma-separated string

                _project.AddNew(project);

                return Ok(new
                {
                    message = "Create Project Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "DeleteProject")]
        public IActionResult DeleteProject(Guid id)
        {
            if (_project.GetProjectById(id) == null)
            {
                return NotFound(new
                {
                    message = "Project not found."
                });
            }
            var project = _project.GetProjectById(id);
            if (project == null)
            {
                return NotFound(new
                {
                    message = "Project not found."
                });
            }

            _project.ChangeStatus(project);


            return Ok(new
            {
                message = "Delete Project Successfully"
            });
        }


    }
}
