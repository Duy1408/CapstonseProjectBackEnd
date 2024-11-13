using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPromotionServices
    {
        bool ChangeStatus(Promotion p);


        List<Promotion> GetPromotions();
        void AddNew(Promotion p);


        Promotion GetPromotionById(Guid id);

        void UpdatePromotion(Promotion p);

        IQueryable<Promotion> SearchPromotion(string name);

        Promotion FindBySalesPolicyIdAndStatus(Guid salePolicyId);

    }
}
