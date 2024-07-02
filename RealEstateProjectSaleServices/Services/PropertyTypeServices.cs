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
        public void AddNew(PropertyType p)
        {
            _type.AddNew(p);
        }

        public bool ChangeStatus(PropertyType p)
        {
           return _type.ChangeStatus(p);
        }

        public List<PropertyType> GetPropertyType()
        {
          return _type.GetPropertyType();
        }

        public PropertyType GetPropertyTypeById(Guid id)
        {
            return _type.GetPropertyTypeById(id);
        }

        public void UpdatePropertyType(PropertyType p)
        {
            _type.UpdatePropertyType(p);
        }
    }
}
