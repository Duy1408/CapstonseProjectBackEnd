using System;
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
            }catch(Exception ex)
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
        [HttpPut("{id}")]
        public IActionResult PutProject(Guid id, Project project)
        {
            if (_project.GetProjects()==null)
            {
                return BadRequest();
            }

          

            try
            {
                 
                _project.UpdateProject(project);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_project.GetProjectById(id)==null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AddNewProject")]
        public IActionResult AddNew([FromForm] ProjectCreateDTO pro)
        {
            try
            {

                var newPro = new ProjectCreateDTO
                {
                   ProjectID = Guid.NewGuid(),
                   ProjectName = pro.ProjectName,
                   CommericalName = pro.CommericalName,
                   ShortName = pro.ShortName,
                   TypeOfProject = pro.TypeOfProject,
                    Address = pro.Address,
                    Commune = pro.Commune,
                    District = pro.District,
                    DepositPrice = pro.DepositPrice,
                    Summary = pro.Summary,
                    LicenseNo = pro.LicenseNo,
                    DateOfIssue = pro.DateOfIssue,
                    CampusArea = pro.CampusArea,
                    PlaceofIssue = pro.PlaceofIssue,
                    Code = pro.Code,
                  


                };

                var project = _mapper.Map<Project>(newPro);
               
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
