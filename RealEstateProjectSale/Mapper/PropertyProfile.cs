using RealEstateProjectSaleBusinessObject.BusinessObject;

namespace RealEstateProjectSale.Mapper
{
    public class PropertyProfile
    {

        public Guid PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string Block { get; set; }
        public int Floor { get; set; }
        public double SizeArea { get; set; }
        public int BedRoom { get; set; }
        public int BathRoom { get; set; }
        public int LivingRoom { get; set; }
        public string? View { get; set; }
        public double InitialPrice { get; set; }
        public double? Discount { get; set; }
        public double? MoneyTax { get; set; }
        public double? MaintenanceCost { get; set; }
        public double TotalPrice { get; set; }
        public IFormFile? Image { get; set; }
        public Guid PropertyTypeID { get; set; }
        public Guid ProjectID { get; set; }
     
    }
}
