using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string? EmailOrPhone { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
