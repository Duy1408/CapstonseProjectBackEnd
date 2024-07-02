﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
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

        public bool ChangeStatus(Property p)
        {
            return _pro.ChangeStatus(p);
        }

        public List<Property> GetProperty()
        {
            return _pro.GetAllProperty();
        }

        public Property GetPropertyById(Guid id)
        {
            return _pro.GetPropertyByID(id);
        }

        public void UpdateProperty(Property p)
        {
            _pro.UpdateProperty(p);
        }

   
    }
}
