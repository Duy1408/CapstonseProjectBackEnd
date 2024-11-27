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
        PropertyTypeDAO dao = new PropertyTypeDAO();

        public bool AddNew(PropertyType type) => dao.AddNew(type);

        public void DeletePropertyTypeByID(Guid id) => dao.DeletePropertyTypeByID(id);

        public List<PropertyType> GetAllPropertyType() => dao.GetAllPropertyType();

        public PropertyType GetPropertyTypeByID(Guid id) => dao.GetPropertyTypeByID(id);

        public List<PropertyType> GetPropertyTypeByPropertyCategoryID(Guid id) => dao.GetPropertyTypeByPropertyCategoryID(id);

        public bool UpdatePropertyType(PropertyType type) => dao.UpdatePropertyType(type);

    }
}
