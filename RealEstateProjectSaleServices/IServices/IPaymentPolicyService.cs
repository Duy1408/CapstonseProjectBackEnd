using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPaymentPolicyService
    {
        List<PaymentPolicy> GetAllPaymentPolicy();
        void AddNewPaymentPolicy(PaymentPolicy policy);
        PaymentPolicy GetPaymentPolicyByID(Guid id);
        void UpdatePaymentPolicy(PaymentPolicy policy);
        bool ChangeStatusPaymentPolicy(PaymentPolicy policy);

    }
}
