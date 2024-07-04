using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class AuthVM
    {
        public Guid AccountID { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
