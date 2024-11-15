using RealEstateProjectSaleBusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Booking
    {
        public Guid BookingID { get; set; }
        public DateTime? DepositedTimed { get; set; }
        public double? DepositedPrice { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? BookingFile { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public Guid? StaffID { get; set; }
        public Staff? Staff { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public OpeningForSale? OpeningForSale { get; set; }
        public Guid? PropertyID { get; set; }
        public Property? Property { get; set; }
        public Guid DocumentTemplateID { get; set; }
        public DocumentTemplate? DocumentTemplate { get; set; }
        public Guid ProjectCategoryDetailID { get; set; }
        public ProjectCategoryDetail? ProjectCategoryDetail { get; set; }
        public List<Contract>? Contracts { get; set; }
        public List<Payment>? Payments { get; set; }
        public List<Notification>? Notifications { get; set; }



    }
}
