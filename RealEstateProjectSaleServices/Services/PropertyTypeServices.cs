using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class PropertyTypeServices : IPropertyTypeServices
    {
        private IPropertyTypeRepo _type;
        public PropertyTypeServices(IPropertyTypeRepo type)
        {
            _type = type;
        }

        public bool AddNew(PropertyType type)
        {
            return _type.AddNew(type);
        }

        public void DeletePropertyTypeByID(Guid id)
        {
            _type.DeletePropertyTypeByID(id);
        }

        public List<PropertyType> GetAllPropertyType()
        {
            return _type.GetAllPropertyType();
        }

        public PropertyType GetPropertyTypeByID(Guid id)
        {
            return _type.GetPropertyTypeByID(id);
        }

        public List<PropertyType> GetPropertyTypeByPropertyCategoryID(Guid id)
        {
            return _type.GetPropertyTypeByPropertyCategoryID(id);
        }

        public bool UpdatePropertyType(PropertyType type)
        {
            return _type.UpdatePropertyType(type);
        }
    }
}
