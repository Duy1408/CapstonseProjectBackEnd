﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IProjectCategoryDetailRepo
    {
        List<ProjectCategoryDetail> GetAllProjectCategoryDetail();
        ProjectCategoryDetail GetProjectCategoryDetailByID(Guid id);
        List<ProjectCategoryDetail> GetProjectCategoryDetailByProjectID(Guid id);
        void AddNewProjectCategoryDetail(ProjectCategoryDetail detail);
        void UpdateProjectCategoryDetail(ProjectCategoryDetail detail);
        void DeleteProjectCategoryDetailByID(Guid id);
        ProjectCategoryDetail GetDetailByProjectIDCategoryID(Guid projectID, Guid propertyCategoryID);

    }
}
