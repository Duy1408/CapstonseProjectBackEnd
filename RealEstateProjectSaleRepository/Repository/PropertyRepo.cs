using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class PropertyRepo : IPropertyRepo
    {
        private PropertyDAO _pro;

        public PropertyRepo()
        {
            _pro = new PropertyDAO();
        }
        public void AddNew(Property p)
        {
            _pro.AddNew(p);
        }
        public List<Property> GetProperty()
        {
            return _pro.GetAllProperty();
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
            return _pro.GetPropertyByID(id);
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
