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
    public class PromotionServices : IPromotionServices
    {
       private readonly IPromotionRepo _pro;
        public PromotionServices(IPromotionRepo pro)
        {
            _pro = pro;
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
            return _pro.GetPromotionById(id);
        }

        public List<Promotion> GetPromotions()
        {
            return _pro.GetPromotions();
        }

        public IQueryable<Promotion> SearchPromotion(string name)
        {
            return _pro.SearchPromotion(name);
        }

        public void UpdatePromotion(Promotion p)
        {
             _pro.UpdatePromotion(p);
        }
    }
}
