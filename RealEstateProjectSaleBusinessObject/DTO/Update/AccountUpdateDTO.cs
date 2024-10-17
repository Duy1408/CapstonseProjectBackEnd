using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class AccountUpdateDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }
        public Guid? RoleID { get; set; }
    }
}
