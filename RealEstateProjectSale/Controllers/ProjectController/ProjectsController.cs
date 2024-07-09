using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
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
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
          if (_project.GetProjects() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.Projects'  is null.");
          }

            try
            {
                _project.AddNew(project);
            }
            catch (DbUpdateException)
            {
                if (_project.GetProjects() == null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjetcs", new { id = project.ProjectID }, project);
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
