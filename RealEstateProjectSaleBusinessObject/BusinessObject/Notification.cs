using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Notification
    {
        public Guid NotificationID { get; set; }
        public string Title { get; set; }
        public string Subtiltle { get; set; }
        public string Body { get; set; }
        public string DeepLink { get; set; }
        public bool Status { get; set; }
        public Guid? CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public Guid? OpenForSaleID { get; set; }
        public OpeningForSale? OpeningForSale { get; set; }



    }
}
