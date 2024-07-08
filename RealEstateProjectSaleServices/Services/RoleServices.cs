using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepo _roleRepo;
        public RoleServices(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public List<Role> GetAllRole() => _roleRepo.GetAllRole();

        public Role GetRoleByID(Guid id) => _roleRepo.GetRoleByID(id);

        public Role GetRoleByRoleName(string roleName) => _roleRepo.GetRoleByRoleName(roleName);

    }
}
