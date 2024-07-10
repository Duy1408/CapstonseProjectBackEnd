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
        List<PropertyType> GetAllPropertyType();

        PropertyType GetPropertyTypeByID(Guid id);

    }
}
