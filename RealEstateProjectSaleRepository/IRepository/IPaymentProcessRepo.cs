using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPaymentProcessRepo
    {
        void DeletePaymentProcessByID(Guid id);


        public List<PaymentProcess> GetPaymentProcess();
        public void AddNew(PaymentProcess p);


        public PaymentProcess GetPaymentProcessById(Guid id);

        public void UpdatePaymentProcess(PaymentProcess p);
        List<PaymentProcess> GetPaymentProcessByProjectID(Guid salesPolicyId);

    }
}
