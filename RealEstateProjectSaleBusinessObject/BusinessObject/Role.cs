using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Role
    {
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public List<Account>? Accounts { get; set; }

    }
}
