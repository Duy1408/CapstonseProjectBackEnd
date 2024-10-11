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
    public class ProjectCategoryDetailServices : IProjectCategoryDetailServices
    {
        private readonly IProjectCategoryDetailRepo _detail;
        public ProjectCategoryDetailServices(IProjectCategoryDetailRepo detail)
        {
            _detail = detail;
        }

        public void AddNewProjectCategoryDetail(ProjectCategoryDetail detail) => _detail.AddNewProjectCategoryDetail(detail);

        public void DeleteProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID) => _detail.DeleteProjectCategoryDetailByID(projectID, propertyCategoryID);

        public List<ProjectCategoryDetail> GetAllProjectCategoryDetail() => _detail.GetAllProjectCategoryDetail();

        public ProjectCategoryDetail GetProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID) => _detail.GetProjectCategoryDetailByID(projectID, propertyCategoryID);

        public List<ProjectCategoryDetail> GetProjectCategoryDetailByProjectID(Guid id) => _detail.GetProjectCategoryDetailByProjectID(id);

        public void UpdateProjectCategoryDetail(ProjectCategoryDetail detail) => _detail.UpdateProjectCategoryDetail(detail);

    }
}
