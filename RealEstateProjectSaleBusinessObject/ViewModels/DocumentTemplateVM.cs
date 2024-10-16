using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
   public class DocumentTemplateVM
    {
        public Guid DocumentTemplateID { get; set; }
        public string DocumentName { get; set; }
        public string? DocumentFile { get; set; }
        public bool Status { get; set; }
    }
}
