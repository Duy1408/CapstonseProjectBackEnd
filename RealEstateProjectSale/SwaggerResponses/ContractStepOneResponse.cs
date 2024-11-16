using RealEstateProjectSaleBusinessObject.ViewModels;

namespace RealEstateProjectSale.SwaggerResponses
{
    public class ContractStepOneResponse
    {
        public CustomerVM Customer { get; set; }
        public PropertyVM Property { get; set; }
        public ProjectVM Project { get; set; }
    }
}
