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
        public int Floor {  get; set; }
        public double SizeArea { get; set; }
        public int BedRoom {  get; set; }
        public int BathRoom { get; set;}
        public int LivingRoom { get; set; }
        public double Price { get; set; }
        public string? View { get; set; }
        public string? Image { get; set; }
   

        public List<Comment>? Comments { get; set; }
        public Guid PropertyTypeID { get; set; }

        public PropertyType? PropertyType { get; set; }

        public List<OpenForSaleDetail>? OpenForSaleDetails { get; set; }
        public Guid ProjectID { get; set; }
        public Project? Project { get; set; }
        public List<Booking>? Bookings { get; set; }

    }
}
