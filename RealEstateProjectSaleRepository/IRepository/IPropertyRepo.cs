using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPropertyRepo
    {

        public List<Property> GetProperty();
        public void AddNew(Property p);


        public Property GetPropertyById(Guid id);

        public void UpdateProperty(Property p);
        IQueryable<Property> GetPropertyByUnitTypeID(Guid id);
        IQueryable<Property> GetPropertyByFloorID(Guid id);
        IQueryable<Property> GetPropertyByBlockID(Guid id);
        IQueryable<Property> GetPropertyByZoneID(Guid id);
        IQueryable<Property> GetPropertyByProjectCategoryDetailID(Guid id);
        IQueryable<Property> SearchPropertyByName(string searchvalue);
        List<Property> GetPropertyByCategoryDetailID(Guid id);
        bool ChangeStatusProperty(Property property);
    }
}
