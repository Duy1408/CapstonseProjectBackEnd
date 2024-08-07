﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
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

        public Property GetPropertyById(Guid id)
        {
            return _pro.GetPropertyById(id);
        }

        public IQueryable<Property> GetPropertyByProjectID(Guid id)
        {
            return _pro.GetPropertyByProjectID(id);
        }

        public IQueryable<Property> GetPropertyByPropertyTypeID(Guid id)
        {
            return _pro.GetPropertyByPropertyTypeID(id);
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
