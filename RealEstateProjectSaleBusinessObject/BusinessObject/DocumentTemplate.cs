using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class DocumentTemplate
    {
        public Guid DocumentID { get; set; }
        public string  DocumentName { get; set; }
        public string?  DocumentFile { get; set; }
        public List<Contract>? Contracts { get; set; }
        public List<Booking>? Bookings { get; set; }


    }
}
