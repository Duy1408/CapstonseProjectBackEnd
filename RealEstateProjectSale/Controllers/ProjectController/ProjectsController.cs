using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
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

        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;




        public ProjectsController(IProjectServices project, BlobServiceClient blobServiceClient, IMapper mapper)
        {
            _project = project;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
        }
        // GET: api/Projects
        [HttpGet]
        [SwaggerOperation(Summary = "Get All Project")]
        public IActionResult GetAllProject()
        {
            try
            {
                var projects = _project.GetProjects();

                if (projects == null || !projects.Any())
                {
                    return NotFound();
                }

                var response = projects.Select(project => new ProjectVM
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

        // GET: api/Projects/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "GetProjectByID")]
        public IActionResult GetProjectByID(Guid id)
        {
            try
            {
                var project = _project.GetProjectById(id);

                if (project == null)
                {
                    return NotFound();
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

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "UpdateProject")]
        public IActionResult UpdateProject([FromForm] ProjectUpdateDTO project, Guid id)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateimage");
                var imageUrls = new List<string>(); // List to hold URLs of all images
                if (project.Images != null && project.Images.Count > 0)
                {

                    foreach (var image in project.Images)
                    {
                        var blobName = $"{Guid.NewGuid()}_{image.FileName}";
                        var blobInstance = containerInstance.GetBlobClient(blobName);
                        blobInstance.Upload(image.OpenReadStream());
                        var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/realestateimage";
                        var blobUrl = $"{storageAccountUrl}/{blobName}";
                        imageUrls.Add(blobUrl); // Add each image URL to the list
                    }
                }

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
                    if (!string.IsNullOrEmpty(project.Status))
                    {
                        existingProject.Status = project.Status;
                    }



                    _project.UpdateProject(existingProject);

                    return Ok("Update Project Successfully");

                }

                return NotFound("Project not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(Summary = "AddNewProject")]
        public IActionResult AddNew([FromForm] ProjectRequestDTO pro)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateimage");
                var imageUrls = new List<string>(); // List to hold URLs of all images
                if (pro.Images != null && pro.Images.Count > 0)
                {

                    foreach (var image in pro.Images)
                    {
                        var blobName = $"{Guid.NewGuid()}_{image.FileName}";
                        var blobInstance = containerInstance.GetBlobClient(blobName);
                        blobInstance.Upload(image.OpenReadStream());
                        var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/realestateimage";
                        var blobUrl = $"{storageAccountUrl}/{blobName}";
                        imageUrls.Add(blobUrl); // Add each image URL to the list
                    }
                    //var blobName = $"{Guid.NewGuid()}_{pro.Image.FileName}";
                    //var blobInstance = containerInstance.GetBlobClient(blobName);
                    //blobInstance.Upload(pro.Image.OpenReadStream());
                    //var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    //blobUrl = $"{storageAccountUrl}/{blobName}";
                }

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
                    Status = ProjectStatus.NotForSale.ToString(),
                    Images = pro.Images.Count > 0 ? pro.Images.First() : null, // Store first image for reference
                };


                var project = _mapper.Map<Project>(newPro);
                //project.Image = blobUrl;
                project.Image = string.Join(",", imageUrls); // Store all image URLs as a comma-separated string

                _project.AddNew(project);

                return Ok("Create Project Successfully");
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
                return NotFound();
            }
            var project = _project.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            _project.ChangeStatus(project);


            return Ok("Delete Successfully");
        }


    }
}
