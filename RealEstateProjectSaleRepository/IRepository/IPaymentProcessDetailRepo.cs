using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPaymentProcessDetailRepo
    {
        PaymentProcessDetail CheckPaymentStage(Guid paymentProcessId, int paymentStage);
        void DeletePaymentProcessDetailByID(Guid id);
        public List<PaymentProcessDetail> GetPaymentProcessDetail();
        public void AddNew(PaymentProcessDetail p);
        public PaymentProcessDetail GetPaymentProcessDetailById(Guid id);
        List<PaymentProcessDetail> GetPaymentProcessDetailByPaymentProcessID(Guid pmtId);
        float GetTotalPercentageByPaymentProcessID(Guid pmtId);
        public void UpdatePaymentProcessDetail(PaymentProcessDetail p);

    }
}
