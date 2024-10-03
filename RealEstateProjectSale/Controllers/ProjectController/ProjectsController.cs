using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace RealEstateProjectSale.Controllers
{
    [Route("api/[controller]")]
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
        [Route("GetAllProjects")]

        public IActionResult GetProjects()
        {
            try
            {
                if (_project.GetProjects() == null)
                {
                    return NotFound();
                }
                var projects = _project.GetProjects();
                var response = _mapper.Map<List<ProjectVM>>(projects);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        // GET: api/Projects/5
        [HttpGet("GetProjectByID/{id}")]
        public IActionResult GetProjectByID(Guid id)
        {
            var project = _project.GetProjectById(id);

            if (project != null)
            {
                var responese = _mapper.Map<ProjectVM>(project);

                return Ok(responese);
            }

            return NotFound();

        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateProject/{id}")]
        public IActionResult UpdateProject([FromForm] ProjectUpdateDTO project, Guid id)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateprojectpictures");
                string? blobUrl = null;
                if (project.Image != null)
                {
                    var blobName1 = $"{Guid.NewGuid()}_{project.Image.FileName}";
                    var blobInstance1 = containerInstance.GetBlobClient(blobName1);
                    blobInstance1.Upload(project.Image.OpenReadStream());
                    var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    blobUrl = $"{storageAccountUrl}/{blobName1}";
                }

                var existingProject = _project.GetProjectById(id);
                if (existingProject != null)
                {

                    if (!string.IsNullOrEmpty(project.ProjectName))
                    {
                        existingProject.ProjectName = project.ProjectName;
                    }
                    //bug
                    //if (!string.IsNullOrEmpty(project.CommericalName))
                    //{
                    //    existingProject.CommericalName = project.CommericalName;
                    //}
                    //if (!string.IsNullOrEmpty(project.ShortName))
                    //{
                    //    existingProject.ShortName = project.ShortName;
                    //}
                    //if (!string.IsNullOrEmpty(project.Address))
                    //{
                    //    existingProject.Address = project.Address;
                    //}
                    //if (!string.IsNullOrEmpty(project.Commune))
                    //{
                    //    existingProject.Commune = project.Commune;
                    //}
                    //if (!string.IsNullOrEmpty(project.District))
                    //{
                    //    existingProject.District = project.District;
                    //}
                    //if (project.DepositPrice.HasValue)
                    //{
                    //    existingProject.DepositPrice = project.DepositPrice.Value;
                    //}
                    //if (!string.IsNullOrEmpty(project.Summary))
                    //{
                    //    existingProject.Summary = project.Summary;
                    //}
                    //if (project.LicenseNo.HasValue)
                    //{
                    //    existingProject.LicenseNo = project.LicenseNo.Value;
                    //}
                    //if (project.DateOfIssue.HasValue)
                    //{
                    //    existingProject.DateOfIssue = project.DateOfIssue.Value;
                    //}
                    //if (!string.IsNullOrEmpty(project.CampusArea))
                    //{
                    //    existingProject.CampusArea = project.CampusArea;
                    //}
                    //if (!string.IsNullOrEmpty(project.PlaceofIssue))
                    //{
                    //    existingProject.PlaceofIssue = project.PlaceofIssue;
                    //}
                    //if (!string.IsNullOrEmpty(project.Code))
                    //{
                    //    existingProject.Code = project.Code;
                    //}
                    if (blobUrl != null)
                    {
                        existingProject.Image = blobUrl;
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
        [Route("AddNewProject")]
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
                    HandOver =pro.HandOver,
                    Convenience =pro.Convenience,
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
        [HttpDelete("DeleteProject/{id}")]
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
