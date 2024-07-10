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

        public List<PropertyType> GetAllPropertyType()
        {
            return _type.GetAllPropertyType();
        }

        public PropertyType GetPropertyTypeByID(Guid id)
        {
            return _type.GetPropertyTypeByID(id);
        }
    }
}
