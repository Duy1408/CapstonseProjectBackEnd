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
                    //.Include(a => a.Properties)
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
                    //.Include(a => a.Properties)
                                               .SingleOrDefault(c => c.PropertyTypeID == id);
                return type;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
