using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Customer
    {
        public Guid CustomerID { get; set; }
        public string FullName { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string Nationality { get; set; }
        public string? PlaceofOrigin { get; set; }
        public string? PlaceOfResidence { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfIssue { get; set; }
        public string? Taxcode { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public Guid AccountID { get; set; }
        public Account? Account { get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<Payment>? Payments { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Contract>? Contracts { get; set; }


    }
}
