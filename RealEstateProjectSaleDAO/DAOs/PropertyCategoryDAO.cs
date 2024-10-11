using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PropertyCategoryDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public PropertyCategoryDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<PropertyCategory> GetAllPropertyCategory()
        {
            try
            {
                return _context.PropertyCategorys!.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PropertyCategory GetPropertyCategoryByID(Guid id)
        {
            try
            {
                var category = _context.PropertyCategorys!.SingleOrDefault(c => c.PropertyCategoryID == id);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewPropertyCategory(PropertyCategory category)
        {
            try
            {
                _context.Add(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public void UpdatePropertyCategory(PropertyCategory category)
        {
            try
            {
                var a = _context.PropertyCategorys!.SingleOrDefault(c => c.PropertyCategoryID == category.PropertyCategoryID);

                _context.Entry(a).CurrentValues.SetValues(category);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusPropertyCategory(PropertyCategory category)
        {
            var _category = _context.PropertyCategorys!.FirstOrDefault(c => c.PropertyCategoryID.Equals(category.PropertyCategoryID));


            if (_category == null)
            {
                return false;
            }
            else
            {
                _category.Status = false;
                _context.Entry(_category).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }


        }
    }
}
