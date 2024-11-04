using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class CustomerUpdateDTO
    {
        public string? FullName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? Nationality { get; set; }
        public string? PlaceofOrigin { get; set; }
        public string? PlaceOfResidence { get; set; }
        public string? DateOfExpiry { get; set; }
        public string? Taxcode { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string? Address { get; set; }
        public string? DeviceToken { get; set; }
        public bool? Status { get; set; }
    }
}
