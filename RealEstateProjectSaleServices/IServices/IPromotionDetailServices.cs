using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPromotionDetailServices
    {
        List<PromotionDetail> GetAllPromotionDetail();
        void AddNewPromotionDetail(PromotionDetail detail);
        PromotionDetail GetPromotionDetailByID(Guid id);
        void UpdatePromotionDetail(PromotionDetail detail);
        void DeletePromotionDetailByID(Guid id);
        PromotionDetail GetDetailByPromotionIDPropertyTypeID(Guid promotionID, Guid propertyTypeID);

    }
}
