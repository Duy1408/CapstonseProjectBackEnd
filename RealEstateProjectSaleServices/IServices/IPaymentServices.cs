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
        Task<PaymentResponseModel> CreatePaymentUrl(PaymentInformationModel payment);

        Session CheckoutSuccess(string sessionId);

        PaymentInformationModel GetPaymentModelFromCache(Guid userId);

    }
}
