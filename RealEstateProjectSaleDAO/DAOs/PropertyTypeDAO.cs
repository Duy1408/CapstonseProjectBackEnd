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
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public PropertyTypeDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<PropertyType> GetAllPropertyType()
        {
            try
            {
                return _context.PropertiesTypes!
                    .Include(a => a.PropertyCategory)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PropertyType GetPropertyTypeByID(Guid id)
        {
            try
            {
                var type = _context.PropertiesTypes!
                                               .Include(a => a.PropertyCategory)
                                               .SingleOrDefault(c => c.PropertyTypeID == id);
                return type;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddNew(PropertyType type)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PropertiesTypes.SingleOrDefault(c => c.PropertyTypeID == type.PropertyTypeID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.PropertiesTypes.Add(type);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdatePropertyType(PropertyType type)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PropertiesTypes.SingleOrDefault(a => a.PropertyTypeID == type.PropertyTypeID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(type);
                _context.SaveChanges();
                return true;
            }
        }

        public void DeletePropertyTypeByID(Guid id)
        {
            var type = _context.PropertiesTypes!.SingleOrDefault(lo => lo.PropertyTypeID == id);
            if (type != null)
            {
                _context.Remove(type);
                _context.SaveChanges();
            }
        }

    }
}
