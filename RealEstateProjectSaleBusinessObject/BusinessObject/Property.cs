using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Property
    {
        public Guid PropertyID { get; set; }
        public string PropertyCode { get; set; }
        public string View { get; set; }
        public double PriceSold { get; set; }

        public string Status { get; set; }

        public List<Comment>? Comments { get; set; }
        public Guid PropertyTypeID { get; set; }

        public PropertyType? PropertyType { get; set; }

        public List<OpenForSaleDetail>? OpenForSaleDetails { get; set; }
        public Guid ProjectID { get; set; }
        public Project? Project { get; set; }
        public List<Booking>? Bookings { get; set; }

    }
}
