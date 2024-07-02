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
     public class PropertyTypeRepo : IPropertyTypeRepo
    {
        private PropertyTypeDAO dao;
        public PropertyTypeRepo()
        {
            dao = new PropertyTypeDAO();
        }
        public void AddNew(PropertyType p)
        {
          dao.AddNew(p);
        }

        public bool ChangeStatus(PropertyType p)
        {
           return dao.ChangeStatus(p);
        }

        public List<PropertyType> GetPropertyType()
        {
          return dao.GetAllPropertyType();
        }

        public PropertyType GetPropertyTypeById(Guid id)
        {
          return dao.GetPropertyTypeByID(id);
        }

        public void UpdatePropertyType(PropertyType p)
        {dao.UpdatePropertyType(p);
        }
    }
}
