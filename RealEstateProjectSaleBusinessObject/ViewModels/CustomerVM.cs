using RealEstateProjectSaleBusinessObject.JsonConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class CustomerVM
    {
        public Guid CustomerID { get; set; }
        public string FullName { get; set; }
        [Column(TypeName = "date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string Nationality { get; set; }
        public string? PlaceofOrigin { get; set; }
        public string? PlaceOfResidence { get; set; }
        public string? DateOfExpiry { get; set; }
        public string? Taxcode { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string Address { get; set; }
        public string? DeviceToken { get; set; }
        public bool Status { get; set; }
        public Guid AccountID { get; set; }
        public string Email { get; set; }
    }
}
