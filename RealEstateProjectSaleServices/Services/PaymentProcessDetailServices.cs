using Azure.Storage.Blobs.Models;
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
    public class PaymentProcessDetailServices : IPaymentProcessDetailServices
    {
        private IPaymentProcessDetailRepo _repo;
        public PaymentProcessDetailServices(IPaymentProcessDetailRepo repo)
        {
            _repo = repo;
        }
        public void AddNew(PaymentProcessDetail p)
        {
            _repo.AddNew(p);
        }

        public void DeletePaymentProcessDetailByID(Guid id)
        {
            _repo.DeletePaymentProcessDetailByID(id);
        }

        public List<PaymentProcessDetail> GetPaymentProcessDetail()
        {
            return _repo.GetPaymentProcessDetail();
        }

        public PaymentProcessDetail GetPaymentProcessDetailById(Guid id)
        {
            return _repo.GetPaymentProcessDetailById(id);
        }

        public void UpdatePaymentProcessDetail(PaymentProcessDetail p)
        {
            _repo.UpdatePaymentProcessDetail(p);
        }
    }
}
