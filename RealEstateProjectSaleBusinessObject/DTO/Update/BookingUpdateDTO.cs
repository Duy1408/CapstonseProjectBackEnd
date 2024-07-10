using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class BookingUpdateDTO
    {
        public IFormFile? BookingFile { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public Guid? PropertyID { get; set; }
        public Guid? StaffID { get; set; }
    }
}
