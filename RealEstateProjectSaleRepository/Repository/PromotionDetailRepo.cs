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
    public class PromotionDetailRepo : IPromotionDetailRepo
    {
        PromotionDetailDAO dao = new PromotionDetailDAO();

        public void AddNewPromotionDetail(PromotionDetail detail) => dao.AddNewPromotionDetail(detail);

        public void DeletePromotionDetailByID(Guid id) => dao.DeletePromotionDetailByID(id);

        public List<PromotionDetail> GetAllPromotionDetail() => dao.GetAllPromotionDetail();

        public PromotionDetail GetDetailByPromotionIDPropertyTypeID(Guid promotionID, Guid propertyTypeID) => dao.GetDetailByPromotionIDPropertyTypeID(promotionID, propertyTypeID);

        public PromotionDetail GetPromotionDetailByID(Guid id) => dao.GetPromotionDetailByID(id);

        public List<PromotionDetail> GetPromotionDetailByPromotionID(Guid id) => dao.GetPromotionDetailByPromotionID(id);

        public void UpdatePromotionDetail(PromotionDetail detail) => dao.UpdatePromotionDetail(detail);

    }
}
