using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PromotionDAO
    {
        private static PromotionDAO instance;

        public static PromotionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PromotionDAO();
                }
                return instance;
            }


        }

        public List<Promotion> GetAllPromotion()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Promotions.Include(c=>c.Salespolicy).ToList();
        }

        public bool AddNew(Promotion p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Promotions.SingleOrDefault(c => c.PromotionID == p.PromotionID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Promotions.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdatePromotion(Promotion p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Promotions.SingleOrDefault(c => c.PromotionID == p.PromotionID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(p);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(Promotion p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Promotions.FirstOrDefault(c => c.PromotionID.Equals(p.PromotionID));


            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }


        public Promotion GetPromotionByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Promotions.Include(c=>c.Salespolicy).SingleOrDefault(a => a.PromotionID == id);
        }
        public IQueryable<Promotion> SearchPromotionByName(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Promotions.Where(a => a.PromotionName.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }
    }
}
