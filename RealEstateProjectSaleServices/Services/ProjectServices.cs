using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class ProjectServices : IProjectServices
    {
        private IProjectRepo _pro;
        public ProjectServices(IProjectRepo pro)
        {
            _pro = pro;
        }
        public void AddNew(Project p)
        {
            _pro.AddNew(p);
        }

        public bool ChangeStatus(Project p)
        {
            return _pro.ChangeStatus(p);
        }

        public Project GetProjectById(Guid id)
        {
           return _pro.GetProjectById(id);
        }

        public List<Project> GetProjects()
        {
            return _pro.GetProjects();
        }

        public IQueryable<Project> SearchProject(string name)
        {
           return _pro.SearchProject(name);
        }

        public void UpdateProject(Project p)
        {
            _pro.UpdateProject(p);
        }
    }
}
