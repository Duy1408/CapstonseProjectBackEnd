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

        public void DeleteProjectCategoryDetailByID(Guid id) => _detail.DeleteProjectCategoryDetailByID(id);

        public List<ProjectCategoryDetail> GetAllProjectCategoryDetail() => _detail.GetAllProjectCategoryDetail();

        public ProjectCategoryDetail GetDetailByProjectIDCategoryID(Guid projectID, Guid propertyCategoryID) => _detail.GetDetailByProjectIDCategoryID(projectID, propertyCategoryID);

        public ProjectCategoryDetail GetProjectCategoryDetailByID(Guid id) => _detail.GetProjectCategoryDetailByID(id);

        public List<ProjectCategoryDetail> GetProjectCategoryDetailByProjectID(Guid id) => _detail.GetProjectCategoryDetailByProjectID(id);

        public void UpdateProjectCategoryDetail(ProjectCategoryDetail detail) => _detail.UpdateProjectCategoryDetail(detail);

    }
}
