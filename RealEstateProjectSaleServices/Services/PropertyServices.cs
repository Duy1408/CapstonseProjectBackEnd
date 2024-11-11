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
    public class PropertyServices : IPropertyServices
    {

        private IPropertyRepo _pro;

        public PropertyServices(IPropertyRepo pro)
        {
            _pro = pro;
        }
        public void AddNew(Property p)
        {
            _pro.AddNew(p);
        }
        public List<Property> GetProperty()
        {
            return _pro.GetProperty();
        }

        public IQueryable<Property> GetPropertyByBlockID(Guid id)
        {
            return _pro.GetPropertyByBlockID(id);
        }

        public IQueryable<Property> GetPropertyByFloorID(Guid id)
        {
            return _pro.GetPropertyByFloorID(id);
        }

        public Property GetPropertyById(Guid? id)
        {
            return _pro.GetPropertyById(id);
        }

        public IQueryable<Property> GetPropertyByProjectCategoryDetailID(Guid id)
        {
            return _pro.GetPropertyByProjectCategoryDetailID(id);
        }

        public IQueryable<Property> GetPropertyByUnitTypeID(Guid id)
        {
            return _pro.GetPropertyByUnitTypeID(id);
        }

        public IQueryable<Property> GetPropertyByZoneID(Guid id)
        {
            return _pro.GetPropertyByZoneID(id);
        }

        public IQueryable<Property> SearchPropertyByName(string searchvalue)
        {
            return _pro.SearchPropertyByName(searchvalue);
        }

        public void UpdateProperty(Property p)
        {
            _pro.UpdateProperty(p);
        }
    }
}
