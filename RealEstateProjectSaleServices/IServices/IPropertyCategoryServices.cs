using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPropertyCategoryServices
    {
        List<PropertyCategory> GetAllPropertyCategory();
        PropertyCategory GetPropertyCategoryByID(Guid id);
        void AddNewPropertyCategory(PropertyCategory category);
        void UpdatePropertyCategory(PropertyCategory category);
        bool ChangeStatusPropertyCategory(PropertyCategory category);
    }
}
