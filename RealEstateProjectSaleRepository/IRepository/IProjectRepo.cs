using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IProjectRepo
    {
        public bool ChangeStatus(Project p);


        public List<Project> GetProjects();
        public void AddNew(Project p);


        public Project GetProjectById(Guid id);

        public void UpdateProject(Project p);

        public IQueryable<Project> SearchProject(string name);
    }
}
