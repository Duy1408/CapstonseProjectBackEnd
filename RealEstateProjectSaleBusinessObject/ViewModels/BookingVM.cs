using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class BookingVM
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
        public string CustomerName { get; set; }
        public Guid? StaffID { get; set; }
        public string StaffName { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public string DecisionName { get; set; }
        public Guid PropertyCategoryID { get; set; }
        public string PropertyCategoryName { get; set; }
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Guid? DocumentTemplateID { get; set; }
        public string DocumentName { get; set; }

    }
}
