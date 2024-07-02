using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPropertyTypeServices
    {
         bool ChangeStatus(PropertyType p);


         List<PropertyType> GetPropertyType();
        void AddNew(PropertyType p);


         PropertyType GetPropertyTypeById(Guid id);

        void UpdatePropertyType(PropertyType p);
    }
}
