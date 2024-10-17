using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class DocumentTemplateUpdateDTO
    {
        public string? DocumentName { get; set; }
        public string? DocumentFile { get; set; }
        public bool? Status { get; set; }
    }
}
