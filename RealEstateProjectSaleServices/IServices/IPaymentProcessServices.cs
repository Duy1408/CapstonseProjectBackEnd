using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPaymentProcessServices
    {
        bool ChangeStatus(PaymentProcess p);


         List<PaymentProcess> GetPaymentProcess();
         void AddNew(PaymentProcess p);


         PaymentProcess GetPaymentProcessById(Guid id);

        void UpdatePaymentProcess(PaymentProcess p);
    }
}
