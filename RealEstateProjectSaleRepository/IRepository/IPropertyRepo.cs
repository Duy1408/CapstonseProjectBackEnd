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
        IQueryable<Property> GetPropertyByProjectID(Guid id);
        IQueryable<Property> GetPropertyByPropertyTypeID(Guid id);
        IQueryable<Property> SearchPropertyByName(string searchvalue);

    }
}
