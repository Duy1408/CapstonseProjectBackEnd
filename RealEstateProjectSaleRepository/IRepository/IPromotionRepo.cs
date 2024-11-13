using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPromotionRepo
    {
        public bool ChangeStatus(Promotion p);


        public List<Promotion> GetPromotions();
        public void AddNew(Promotion p);
        public Promotion GetPromotionById(Guid id);

        public void UpdatePromotion(Promotion p);

        public IQueryable<Promotion> SearchPromotion(string name);

        Promotion FindBySalesPolicyIdAndStatus(Guid salePolicyId);
    }
}
