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
        List<ProjectCategoryDetail> GetProjectCategoryDetailByProjectID(Guid id);
        void AddNewProjectCategoryDetail(ProjectCategoryDetail detail);
        void UpdateProjectCategoryDetail(ProjectCategoryDetail detail);
        void DeleteProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID);
        ProjectCategoryDetail GetProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID);

    }
}
