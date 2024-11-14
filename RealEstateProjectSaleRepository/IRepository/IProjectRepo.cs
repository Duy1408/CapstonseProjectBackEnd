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
        bool ChangeStatus(Project p);
        List<Project> GetProjects();
        void AddNew(Project p);
        Project GetProjectById(Guid id);
        void UpdateProject(Project p);
        IQueryable<Project> SearchProject(string name);
        List<Project> GetProjectByPaymentPolicyID(Guid paymentPolicyId);
    }
}
