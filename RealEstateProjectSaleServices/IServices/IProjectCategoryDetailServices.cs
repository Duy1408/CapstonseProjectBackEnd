using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IProjectCategoryDetailServices
    {
        List<ProjectCategoryDetail> GetAllProjectCategoryDetail();
        ProjectCategoryDetail GetProjectCategoryDetailByID(Guid id);
        List<ProjectCategoryDetail> GetProjectCategoryDetailByProjectID(Guid id);
        void AddNewProjectCategoryDetail(ProjectCategoryDetail detail);
        void UpdateProjectCategoryDetail(ProjectCategoryDetail detail);
        void DeleteProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID);
        ProjectCategoryDetail GetProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID);


    }
}
