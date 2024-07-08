using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IRoleRepo
    {
        List<Role> GetAllRole();
        Role GetRoleByID(Guid id);
        Role GetRoleByRoleName(string roleName);

    }
}
