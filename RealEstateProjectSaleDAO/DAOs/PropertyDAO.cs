using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PropertyDAO
    {
        private static PropertyDAO instance;

        public static PropertyDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PropertyDAO();
                }
                return instance;
            }


        }

        public List<Property> GetAllProperty()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Properties.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                      .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                        .Include(o => o.ProjectCategoryDetail)
                                         .ThenInclude(pc => pc.PropertyCategory)
                                      .ToList();
        }

        public bool AddNew(Property p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties.SingleOrDefault(c => c.PropertyID == p.PropertyID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Properties.Add(p);
                _context.SaveChanges();
                return true;

            }
        }


        public bool UpdateProperty(Property p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties.SingleOrDefault(c => c.PropertyID == p.PropertyID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(p);
                _context.SaveChanges();
                return true;
            }
        }

        public Property GetPropertyByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Properties.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                      .Include(o => o.ProjectCategoryDetail)
                                       .ThenInclude(pc => pc.Project)
                                        .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)

                                      .SingleOrDefault(a => a.PropertyID == id);
        }

        public IQueryable<Property> GetPropertyByUnitTypeID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties!.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                       .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                          .Include(o => o.ProjectCategoryDetail)
                                          .ThenInclude(pc => pc.PropertyCategory)

                                      .Where(c => c.UnitTypeID == id);

            return a;
        }

        public IQueryable<Property> GetPropertyByFloorID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties!.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
.Include(o => o.ProjectCategoryDetail)
    .ThenInclude(pc => pc.Project)
.Include(o => o.ProjectCategoryDetail)
    .ThenInclude(pc => pc.PropertyCategory)

                                      .Where(c => c.FloorID == id);

            return a;
        }

        public IQueryable<Property> GetPropertyByBlockID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties!.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                       .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                        .Include(o => o.ProjectCategoryDetail)
                                         .ThenInclude(pc => pc.PropertyCategory)

                                      .Where(c => c.BlockID == id);

            return a;
        }

        public IQueryable<Property> GetPropertyByZoneID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties!.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                         .Include(o => o.ProjectCategoryDetail)
                                         .ThenInclude(pc => pc.Project)
                                           .Include(o => o.ProjectCategoryDetail)
                                            .ThenInclude(pc => pc.PropertyCategory)

                                      .Where(c => c.ZoneID == id);

            return a;
        }

        public IQueryable<Property> GetPropertyByProjectCategoryDetailID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties!.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                         .Include(o => o.ProjectCategoryDetail)
                                         .ThenInclude(pc => pc.Project)
                                           .Include(o => o.ProjectCategoryDetail)
                                            .ThenInclude(pc => pc.PropertyCategory)
                                      .Where(c => c.ProjectCategoryDetailID == id && c.Status == PropertyStatus.MoBan.GetEnumDescription());

            return a;
        }

        public IQueryable<Property> SearchPropertyByName(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                .Include(o => o.ProjectCategoryDetail)
                                      .ThenInclude(pc => pc.Project)
                                      .Include(o => o.ProjectCategoryDetail)
                                      .ThenInclude(pc => pc.PropertyCategory)
                                      .Where(a => a.PropertyCode.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

        public int GetPropertyCountByStatus(string status)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Properties.Include(c => c.UnitType)
                                      .Include(c => c.Floor)
                                      .Include(c => c.Block)
                                      .Include(c => c.Zone)
                                .Include(o => o.ProjectCategoryDetail)
                                      .ThenInclude(pc => pc.Project)
                                      .Include(o => o.ProjectCategoryDetail)
                                      .ThenInclude(pc => pc.PropertyCategory)
                                      .Count(p => p.Status == status);
        }

    }
}
