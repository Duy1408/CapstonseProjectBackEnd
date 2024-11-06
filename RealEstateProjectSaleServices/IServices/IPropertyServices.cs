using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPropertyServices
    {

        List<Property> GetProperty();
        void AddNew(Property p);

        Property GetPropertyById(Guid id);

        void UpdateProperty(Property p);
        IQueryable<Property> GetPropertyByUnitTypeID(Guid id);
        IQueryable<Property> GetPropertyByFloorID(Guid id);
        IQueryable<Property> GetPropertyByBlockID(Guid id);
        IQueryable<Property> GetPropertyByZoneID(Guid id);
        IQueryable<Property> GetPropertyByProjectCategoryDetailID(Guid id);
        IQueryable<Property> SearchPropertyByName(string searchvalue);
    }
}
