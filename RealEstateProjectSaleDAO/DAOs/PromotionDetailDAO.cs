using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PromotionDetailDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public PromotionDetailDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<PromotionDetail> GetAllPromotionDetail()
        {
            try
            {
                return _context.PromotionDetails!.Include(c => c.Promotion)
                                                 .Include(c => c.PropertyType)
                                                 .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewPromotionDetail(PromotionDetail detail)
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

        public PromotionDetail GetPromotionDetailByID(Guid id)
        {
            try
            {
                var detail = _context.PromotionDetails!.Include(c => c.Promotion)
                                                       .Include(c => c.PropertyType)
                                                       .SingleOrDefault(c => c.PromotionDetailID == id);
                return detail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePromotionDetail(PromotionDetail detail)
        {
            try
            {
                var a = _context.PromotionDetails!.SingleOrDefault(c => c.PromotionDetailID == detail.PromotionDetailID);

                _context.Entry(a).CurrentValues.SetValues(detail);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeletePromotionDetailByID(Guid id)
        {
            var detail = _context.PromotionDetails!.SingleOrDefault(lo => lo.PromotionDetailID == id);
            if (detail != null)
            {
                _context.Remove(detail);
                _context.SaveChanges();
            }
        }

    }
}
