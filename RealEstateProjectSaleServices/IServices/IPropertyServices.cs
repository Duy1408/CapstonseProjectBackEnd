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

        IQueryable<Property> GetPropertyByProjectID(Guid id);

        IQueryable<Property> GetPropertyByPropertyTypeID(Guid id);

        IQueryable<Property> SearchPropertyByName(string searchvalue);
    }
}
