using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Model
{
    public class UploadPaymentOrder
    {
        public IFormFile RemittanceOrder { get; set; }
    }
}
