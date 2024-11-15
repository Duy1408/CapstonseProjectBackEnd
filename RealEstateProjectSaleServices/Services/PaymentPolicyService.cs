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
    public class PaymentPolicyService : IPaymentPolicyService
    {
        private readonly IPaymentPolicyRepo _policyRepo;
        public PaymentPolicyService(IPaymentPolicyRepo policyRepo)
        {
            _policyRepo = policyRepo;
        }

        public void AddNewPaymentPolicy(PaymentPolicy policy) => _policyRepo.AddNewPaymentPolicy(policy);

        public bool ChangeStatusPaymentPolicy(PaymentPolicy policy) => _policyRepo.ChangeStatusPaymentPolicy(policy);

        public List<PaymentPolicy> GetAllPaymentPolicy() => _policyRepo.GetAllPaymentPolicy();

        public PaymentPolicy GetPaymentPolicyByID(Guid id) => _policyRepo.GetPaymentPolicyByID(id);

        public void UpdatePaymentPolicy(PaymentPolicy policy) => _policyRepo.UpdatePaymentPolicy(policy);

    }
}
