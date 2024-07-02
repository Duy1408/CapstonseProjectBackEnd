using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class ProjectRepo : IProjectRepo
    {
        private ProjectDAO _pro;
        public ProjectRepo()
        {
            _pro = new ProjectDAO();
        }
        public void AddNew(Project p)
        {
            _pro.AddNew(p);
        }

        public bool ChangeStatus(Project p)
        {
          return  _pro.ChangeStatus(p);
        }

        public Project GetProjectById(Guid id)
        {
            return _pro.GetProjectByID(id);
        }

        public List<Project> GetProjects()
        {
            return _pro.GetAllProject();
        }

        public IQueryable<Project> SearchProject(string name)
        {
            return _pro.SearchProjectByName(name);
        }

        public void UpdateProject(Project p)
        {
             _pro.Update(p);
        }
    }
}
