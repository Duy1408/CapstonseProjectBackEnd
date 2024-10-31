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

        public string? Email { get; set; }

        public string? Password { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }

        public Guid RoleID { get; set; }
    }
}
