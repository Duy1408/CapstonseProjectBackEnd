using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Model;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPaymentServices
    {
        List<Payment> GetAllPayment();
        Payment GetPaymentByID(Guid id);
        void AddNewPayment(Payment payment);
        void UpdatePayment(Payment payment);
        bool ChangeStatusPayment(Payment payment);

        Task<PaymentResponseModel> CreatePaymentUrl(PaymentInformationModel payment);

        Session CheckoutSuccess(string sessionId);

        PaymentInformationModel GetPaymentModelFromCache(Guid userId);

    }
}
