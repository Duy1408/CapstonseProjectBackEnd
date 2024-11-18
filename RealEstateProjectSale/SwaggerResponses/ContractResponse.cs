namespace RealEstateProjectSale.SwaggerResponses
{
    public class ContractResponse
    {
        public Guid ContractID { get; set; }
        public string ProjectName { get; set; }
        public string PropertyCode { get; set; }
        public double? PriceSold { get; set; }
        public string Status { get; set; }
    }
}
