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
    public class OpenForSaleDetailServices : IOpenForSaleDetailServices
    {
        private readonly IOpenForSaleDetailRepo _detailRepo;
        public OpenForSaleDetailServices(IOpenForSaleDetailRepo detailRepo)
        {
            _detailRepo = detailRepo;
        }

        public void AddNewOpenForSaleDetail(OpenForSaleDetail detail) => _detailRepo.AddNewOpenForSaleDetail(detail);

        public void DeleteOpenForSaleDetailByID(Guid id) => _detailRepo.DeleteOpenForSaleDetailByID(id);

        public List<OpenForSaleDetail> GetAllOpenForSaleDetail() => _detailRepo.GetAllOpenForSaleDetail();

        public OpenForSaleDetail GetDetailByPropertyIdOpenId(Guid propertyId, Guid openId) => _detailRepo.GetDetailByPropertyIdOpenId(propertyId, openId);

        public OpenForSaleDetail GetOpenForSaleDetailByID(Guid id) => _detailRepo.GetOpenForSaleDetailByID(id);

        public List<OpenForSaleDetail> GetOpenForSaleDetailByOpeningForSaleID(Guid id) => _detailRepo.GetOpenForSaleDetailByOpeningForSaleID(id);

        public void UpdateOpenForSaleDetail(OpenForSaleDetail detail) => _detailRepo.UpdateOpenForSaleDetail(detail);

    }
}
