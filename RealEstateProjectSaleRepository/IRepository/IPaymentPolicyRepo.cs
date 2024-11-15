using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPaymentPolicyRepo
    {
        List<PaymentPolicy> GetAllPaymentPolicy();
        void AddNewPaymentPolicy(PaymentPolicy policy);
        PaymentPolicy GetPaymentPolicyByID(Guid id);
        void UpdatePaymentPolicy(PaymentPolicy policy);
        bool ChangeStatusPaymentPolicy(PaymentPolicy policy);

    }
}
