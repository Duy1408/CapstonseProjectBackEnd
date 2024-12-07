using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class OpenForSaleDetailDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public OpenForSaleDetailDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<OpenForSaleDetail> GetAllOpenForSaleDetail()
        {
            try
            {
                return _context.OpenForSaleDetails!.Include(c => c.OpeningForSale)
                                                   .Include(c => c.Property)
                                                   .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewOpenForSaleDetail(OpenForSaleDetail detail)
        {
            try
            {
                _context.Add(detail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OpenForSaleDetail GetDetailByPropertyIdOpenId(Guid propertyId, Guid openId)
        {
            try
            {
                var details = _context.OpenForSaleDetails!.Include(c => c.OpeningForSale)
                                                          .Include(c => c.Property)
                                                          .FirstOrDefault(c => c.PropertyID == propertyId
                                                             && c.OpeningForSaleID == openId);
                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<OpenForSaleDetail> GetOpenForSaleDetailByOpeningForSaleID(Guid id)
        {
            try
            {
                var details = _context.OpenForSaleDetails!.Include(c => c.OpeningForSale)
                                                          .Include(c => c.Property)
                                                          .Where(c => c.OpeningForSaleID == id)
                                                          .ToList();
                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOpenForSaleDetail(OpenForSaleDetail detail)
        {
            try
            {
                var a = _context.OpenForSaleDetails!.SingleOrDefault(c => c.OpeningForSaleID == detail.OpeningForSaleID);

                _context.Entry(a).CurrentValues.SetValues(detail);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOpenForSaleDetailByID(Guid propertyId, Guid openId)
        {
            var _detail = _context.OpenForSaleDetails!.SingleOrDefault(lo => lo.OpeningForSaleID == openId && lo.PropertyID == propertyId);
            if (_detail != null)
            {
                _context.Remove(_detail);
                _context.SaveChanges();
            }
        }

    }
}
