using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class OpeningForSaleDAO
    {

        private static OpeningForSaleDAO instance;

        public static OpeningForSaleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OpeningForSaleDAO();
                }
                return instance;
            }


        }

        public List<OpeningForSale> GetAllOppeningForSale()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.OpeningForSales.Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.Project)
                                           .Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.PropertyCategory)
                                           .ToList();
        }

        public bool AddNew(OpeningForSale o)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.OpeningForSales.SingleOrDefault(c => c.OpeningForSaleID == o.OpeningForSaleID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.OpeningForSales.Add(o);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdateOpeningForSale(OpeningForSale o)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.OpeningForSales.SingleOrDefault(c => c.OpeningForSaleID == o.OpeningForSaleID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(o);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(OpeningForSale o)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.OpeningForSales.FirstOrDefault(c => c.OpeningForSaleID.Equals(o.OpeningForSaleID));


            if (a == null)
            {
                return false;
            }
            else
            {
                a.Status = false;
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

        public OpeningForSale FindByDetailIdAndStatus(Guid detailId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.OpeningForSales.Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.Project)
                                           .Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.PropertyCategory)
                                           .SingleOrDefault(a => a.ProjectCategoryDetailID == detailId && a.Status == true);
        }

        public OpeningForSale FindByProjectIdAndStatus(Guid projectId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.OpeningForSales.Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.Project)
                                           .Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.PropertyCategory)
                                           .FirstOrDefault(a => a.ProjectCategoryDetail.ProjectID == projectId && a.Status == true);
        }

        public OpeningForSale GetOpeningForSaleByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.OpeningForSales.Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.Project)
                                           .Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.PropertyCategory)
                                           .SingleOrDefault(a => a.OpeningForSaleID == id);
        }

        public IQueryable<OpeningForSale> GetOpeningForSaleByProjectCategoryDetailID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.OpeningForSales!.Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.Project)
                                             .Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.PropertyCategory)
                                             .Where(c => c.ProjectCategoryDetailID == id);
            return a;
        }

        public bool GetExistOpenStatusByProjectCategoryDetailID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.OpeningForSales!.Any(o => o.ProjectCategoryDetailID == id);
        }

        public IQueryable<OpeningForSale> SearchOpeningForSaleByName(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.OpeningForSales.Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.Project)
                                             .Include(o => o.ProjectCategoryDetail)
                                                .ThenInclude(pc => pc.PropertyCategory)
                                            .Where(a => a.DecisionName.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

    }
}
