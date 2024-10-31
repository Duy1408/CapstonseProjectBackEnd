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
    public class ProjectCategoryDetailRepo : IProjectCategoryDetailRepo
    {
        ProjectCategoryDetailDAO dao = new ProjectCategoryDetailDAO();

        public void AddNewProjectCategoryDetail(ProjectCategoryDetail detail) => dao.AddNewProjectCategoryDetail(detail);

        public void DeleteProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID) => dao.DeleteProjectCategoryDetailByID(projectID, propertyCategoryID);

        public List<ProjectCategoryDetail> GetAllProjectCategoryDetail() => dao.GetAllProjectCategoryDetail();

        public ProjectCategoryDetail GetProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID) => dao.GetProjectCategoryDetailByID(projectID, propertyCategoryID);

        public ProjectCategoryDetail GetProjectCategoryDetailByID(Guid id) => dao.GetProjectCategoryDetailByID(id);

        public List<ProjectCategoryDetail> GetProjectCategoryDetailByProjectID(Guid id) => dao.GetProjectCategoryDetailByProjectID(id);

        public void UpdateProjectCategoryDetail(ProjectCategoryDetail detail) => dao.UpdateProjectCategoryDetail(detail);

    }
}
