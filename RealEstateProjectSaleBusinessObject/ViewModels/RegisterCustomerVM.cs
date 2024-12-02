using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class RegisterCustomerVM
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPass { get; set; }
        public string? FullName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? Nationality { get; set; }
        public string? PlaceOfOrigin { get; set; }
        public string? PlaceOfResidence { get; set; }
        public string? DateOfExpiry { get; set; }
        public string? Taxcode { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string? Address { get; set; }

    }
}
