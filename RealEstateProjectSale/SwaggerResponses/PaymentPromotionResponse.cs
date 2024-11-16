using RealEstateProjectSaleBusinessObject.ViewModels;

namespace RealEstateProjectSale.SwaggerResponses
{
    public class PaymentPromotionResponse
    {
        public PromotionDetailVM PromotionDetail { get; set; }
        public List<PaymentProcessVM> PaymentProcess { get; set; }
    }
}
