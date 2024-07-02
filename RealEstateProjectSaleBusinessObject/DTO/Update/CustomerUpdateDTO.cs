using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class CustomerUpdateDTO
    {
        [JsonIgnore]
        public Guid CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Nationality { get; set; }
        public string? Taxcode { get; set; }
        public string? BankName { get; set; }
        public int? BankNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        [JsonIgnore]
        public Guid AccountID { get; set; }
    }
}
