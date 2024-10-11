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
    public class PropertyCategoryRepo : IPropertyCategoryRepo
    {
        PropertyCategoryDAO dao = new PropertyCategoryDAO();

        public void AddNewPropertyCategory(PropertyCategory category) => dao.AddNewPropertyCategory(category);

        public bool ChangeStatusPropertyCategory(PropertyCategory category) => dao.ChangeStatusPropertyCategory(category);

        public List<PropertyCategory> GetAllPropertyCategory() => dao.GetAllPropertyCategory();

        public PropertyCategory GetPropertyCategoryByID(Guid id) => dao.GetPropertyCategoryByID(id);

        public void UpdatePropertyCategory(PropertyCategory category) => dao.UpdatePropertyCategory(category);

    }
}
