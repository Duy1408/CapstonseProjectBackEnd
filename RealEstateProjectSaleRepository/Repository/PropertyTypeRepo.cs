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

        public List<PropertyType> GetAllPropertyType() => dao.GetAllPropertyType();

        public PropertyType GetPropertyTypeByID(Guid id) => dao.GetPropertyTypeByID(id);

    }
}
