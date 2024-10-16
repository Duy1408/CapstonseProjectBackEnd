using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class DocumentTemplateCreateDTO
    {
        [JsonIgnore]
        public Guid DocumentTemplateID { get; set; }
        public string DocumentName { get; set; }
        public IFormFile DocumentFile { get; set; }
        public bool Status { get; set; }
    }
}
