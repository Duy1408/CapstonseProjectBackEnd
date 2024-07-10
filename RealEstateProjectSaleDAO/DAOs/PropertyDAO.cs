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

    }
}
