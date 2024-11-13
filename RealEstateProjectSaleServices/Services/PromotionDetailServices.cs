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
    public class PromotionDetailServices : IPromotionDetailServices
    {
        private readonly IPromotionDetailRepo _detailRepo;
        public PromotionDetailServices(IPromotionDetailRepo detailRepo)
        {
            _detailRepo = detailRepo;
        }

        public void AddNewPromotionDetail(PromotionDetail detail) => _detailRepo.AddNewPromotionDetail(detail);

        public void DeletePromotionDetailByID(Guid id) => _detailRepo.DeletePromotionDetailByID(id);

        public List<PromotionDetail> GetAllPromotionDetail() => _detailRepo.GetAllPromotionDetail();

        public PromotionDetail GetDetailByPromotionIDPropertyTypeID(Guid promotionID, Guid propertyTypeID) => _detailRepo.GetDetailByPromotionIDPropertyTypeID(promotionID, propertyTypeID);

        public PromotionDetail GetPromotionDetailByID(Guid id) => _detailRepo.GetPromotionDetailByID(id);

        public void UpdatePromotionDetail(PromotionDetail detail) => _detailRepo.UpdatePromotionDetail(detail);

    }
}
