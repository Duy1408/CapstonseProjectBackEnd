using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class AccountCreateDTO
    {
        [JsonIgnore]
        public Guid AccountID { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }

        //[Required(ErrorMessage = "Role is required.")]
        public Guid RoleID { get; set; }
    }
}
