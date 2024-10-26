using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class NotificationVM
    {
        public Guid NotificationID { get; set; }
        public string Title { get; set; }
        public string Subtiltle { get; set; }
        public string Body { get; set; }
        public string DeepLink { get; set; }
        public bool Status { get; set; }
        public Guid? CustomerID { get; set; }
        public string FullName { get; set; }
        public Guid? OpeningForSaleID { get; set; }
        public string DecisionName { get; set; }
    }
}
