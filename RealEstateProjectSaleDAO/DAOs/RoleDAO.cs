using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class RoleDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public RoleDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<Role> GetAllRole()
        {
            try
            {
                return _context.Roles!.Include(a => a.Accounts).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Role GetRoleByID(Guid id)
        {
            try
            {
                var role = _context.Roles!.Include(a => a.Accounts)
                                               .SingleOrDefault(c => c.RoleID == id);
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Role GetRoleByRoleName(string roleName)
        {
            try
            {
                var role = _context.Roles!.Include(a => a.Accounts)
                                               .SingleOrDefault(c => c.RoleName == roleName);
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
