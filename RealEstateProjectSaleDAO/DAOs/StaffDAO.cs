using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class StaffDAO
    {

        private readonly RealEstateProjectSaleSystemDBContext _context;
        public StaffDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<Staff> GetAllStaff()
        {
            try
            {
                return _context.Staffs!.Include(c => c.Account)
                                       .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewStaff(Staff staff)
        {
            try
            {
                _context.Add(staff);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Staff GetStaffByID(Guid id)
        {
            try
            {
                var staff = _context.Staffs!.Include(a => a.Account)
                                           .SingleOrDefault(c => c.StaffID == id);
                return staff;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStaff(Staff staff)
        {
            try
            {
                var a = _context.Staffs!.SingleOrDefault(c => c.StaffID == staff.StaffID);

                _context.Entry(a).CurrentValues.SetValues(staff);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusStaff(Staff staff)
        {
            var _staff = _context.Staffs!.FirstOrDefault(c => c.StaffID.Equals(staff.StaffID));


            if (_staff == null)
            {
                return false;
            }
            else
            {
                _staff.Status = false;
                _context.Entry(_staff).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
