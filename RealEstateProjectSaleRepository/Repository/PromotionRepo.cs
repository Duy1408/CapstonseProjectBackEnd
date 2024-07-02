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
    public class PromotionRepo : IPromotionRepo
    {
        private PromotionDAO _pro;
     
        public PromotionRepo()
        {
            _pro = new PromotionDAO();
        }
        public void AddNew(Promotion p)
        {
            _pro.AddNew(p);
        }

        public bool ChangeStatus(Promotion p)
        {
            return _pro.ChangeStatus(p);
        }

        public Promotion GetPromotionById(Guid id)
        {
            return _pro.GetPromotionByID(id);
        }

        public List<Promotion> GetPromotions()
        {
            return _pro.GetAllPromotion();
        }

        public IQueryable<Promotion> SearchPromotion(string name)
        {
            return _pro.SearchPromotionByName(name);
        }

        public void UpdatePromotion(Promotion p)
        {
             _pro.UpdatePromotion(p);
        }
    }
}
