using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IRoleServices
    {
        List<Role> GetAllRole();
        Role GetRoleByID(Guid id);
        Role GetRoleByRoleName(string roleName);

    }
}
