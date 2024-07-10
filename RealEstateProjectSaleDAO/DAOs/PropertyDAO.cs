using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
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
            return _context.Properties.Include(p => p.PropertyType)
                                      .Include(p => p.Project)
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
            return _context.Properties.Include(p => p.PropertyType)
                                      .Include(p => p.Project)
                                      .SingleOrDefault(a => a.PropertyID == id);
        }

        public IQueryable<Property> GetPropertyByProjectID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties!.Include(c => c.PropertyType)
                                    .Include(c => c.Project)
                                    .Where(c => c.ProjectID == id);
            return a;
        }

        public IQueryable<Property> GetPropertyByPropertyTypeID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties!.Include(c => c.PropertyType)
                                    .Include(c => c.Project)
                                    .Where(c => c.PropertyTypeID == id);
            return a;
        }

        public IQueryable<Property> SearchPropertyByName(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Properties.Where(a => a.PropertyName.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

    }
}
