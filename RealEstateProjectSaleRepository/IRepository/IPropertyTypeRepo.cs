using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPropertyTypeRepo
    {
        public bool ChangeStatus(PropertyType p);


        public List<PropertyType> GetPropertyType();
        public void AddNew(PropertyType p);


        public PropertyType GetPropertyTypeById(Guid id);

        public void UpdatePropertyType(PropertyType p);
    }
}
