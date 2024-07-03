using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PropertyTypeDAO
    {
        private static PropertyTypeDAO instance;

        public static PropertyTypeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PropertyTypeDAO();
                }
                return instance;
            }


        }

        public List<PropertyType> GetAllPropertyType()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PropertiesTypes.ToList();
        }

        public bool AddNew(PropertyType p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PropertiesTypes.SingleOrDefault(c => c.PropertyTypeID == p.PropertyTypeID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.PropertiesTypes.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdatePropertyType(PropertyType p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PropertiesTypes.SingleOrDefault(c => c.PropertyTypeID == p.PropertyTypeID);

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

        public bool ChangeStatus(PropertyType p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PropertiesTypes.FirstOrDefault(c => c.PropertyTypeID.Equals(p.PropertyTypeID));


            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }



        public PropertyType GetPropertyTypeByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PropertiesTypes.SingleOrDefault(a => a.PropertyTypeID == id);
        }
        public IQueryable<PropertyType> SearchPropertyTypeByName(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PropertiesTypes.Where(a => a.TypeName.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

    }
}
