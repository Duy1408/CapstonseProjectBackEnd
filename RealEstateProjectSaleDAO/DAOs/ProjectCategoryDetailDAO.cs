﻿using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ProjectCategoryDetailDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public ProjectCategoryDetailDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<ProjectCategoryDetail> GetAllProjectCategoryDetail()
        {
            try
            {
                return _context.ProjectCategoryDetails!.Include(c => c.Project)
                                                       .Include(c => c.PropertyCategory)
                                                       .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProjectCategoryDetail> GetProjectCategoryDetailByProjectID(Guid id)
        {
            try
            {
                var details = _context.ProjectCategoryDetails!.Include(c => c.Project)
                                                             .Include(c => c.PropertyCategory)
                                                             .Where(c => c.ProjectID == id)
                                                             .ToList();
                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProjectCategoryDetail GetProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID)
        {
            try
            {
                var details = _context.ProjectCategoryDetails!.Include(c => c.Project)
                                                             .Include(c => c.PropertyCategory)
                                                             .SingleOrDefault(c => c.ProjectID == projectID
                                                             && c.PropertyCategoryID == propertyCategoryID);
                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewProjectCategoryDetail(ProjectCategoryDetail detail)
        {
            try
            {
                _context.Add(detail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public void UpdateProjectCategoryDetail(ProjectCategoryDetail detail)
        {
            try
            {
                var a = _context.ProjectCategoryDetails!.SingleOrDefault(c => c.ProjectID == detail.ProjectID);

                _context.Entry(a).CurrentValues.SetValues(detail);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProjectCategoryDetailByID(Guid projectID, Guid propertyCategoryID)
        {
            var detail = _context.ProjectCategoryDetails!
                        .SingleOrDefault(lo => lo.ProjectID == projectID && lo.PropertyCategoryID == propertyCategoryID);
            if (detail != null)
            {
                _context.Remove(detail);
                _context.SaveChanges();
            }
        }

    }
}