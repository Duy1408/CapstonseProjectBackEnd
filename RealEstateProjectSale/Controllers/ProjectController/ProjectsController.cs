using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectServices _project;

        public ProjectsController(IProjectServices project)
        {
            _project = project;
            
        }
        // GET: api/Projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
          if (_project.GetProjects() == null)
          {
              return NotFound();
          }
            return _project.GetProjects().ToList();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(Guid id)
        {
          if (_project.GetProjects() == null)
          {
              return NotFound();
          }
            var project = _project.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            if (_project.GetProjects() == null)
            {
                return NotFound();
            }
            var project =  _project.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            _project.ChangeStatus(project);

            return NoContent();
        }

   
    }
}
