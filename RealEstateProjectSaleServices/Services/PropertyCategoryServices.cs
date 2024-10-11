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
    public class PropertyCategoryServices : IPropertyCategoryServices
    {
        private readonly IPropertyCategoryRepo _categoryRepo;
        public PropertyCategoryServices(IPropertyCategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void AddNewPropertyCategory(PropertyCategory category) => _categoryRepo.AddNewPropertyCategory(category);

        public bool ChangeStatusPropertyCategory(PropertyCategory category) => _categoryRepo.ChangeStatusPropertyCategory(category);

        public List<PropertyCategory> GetAllPropertyCategory() => _categoryRepo.GetAllPropertyCategory();

        public PropertyCategory GetPropertyCategoryByID(Guid id) => _categoryRepo.GetPropertyCategoryByID(id);

        public void UpdatePropertyCategory(PropertyCategory category) => _categoryRepo.UpdatePropertyCategory(category);

    }
}
