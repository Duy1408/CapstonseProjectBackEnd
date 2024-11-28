using RealEstateProjectSaleBusinessObject.JsonConverters;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateProjectSale.SwaggerResponses
{
    public class ContractResponse
    {
        public Guid ContractID { get; set; }
        public string ProjectName { get; set; }
        public string PropertyCode { get; set; }
        public double? PriceSold { get; set; }
        [Column(TypeName = "date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? ExpiredTime { get; set; }
        public string Status { get; set; }
    }
}
