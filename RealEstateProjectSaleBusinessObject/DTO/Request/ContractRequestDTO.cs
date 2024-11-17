using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Request
{
    public class ContractRequestDTO
    {
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        public IFormFile? ContractDepositFile { get; set; }
        public Guid BookingID { get; set; }

    }
}
