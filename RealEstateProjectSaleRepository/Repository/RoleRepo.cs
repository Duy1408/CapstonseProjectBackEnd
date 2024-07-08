using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class RoleRepo : IRoleRepo
    {
        RoleDAO dao = new RoleDAO();

        public List<Role> GetAllRole() => dao.GetAllRole();

        public Role GetRoleByID(Guid id) => dao.GetRoleByID(id);

        public Role GetRoleByRoleName(string roleName) => dao.GetRoleByRoleName(roleName);

    }
}
