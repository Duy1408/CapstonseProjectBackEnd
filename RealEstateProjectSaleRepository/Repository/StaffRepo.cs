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
    public class StaffRepo : IStaffRepo
    {
        StaffDAO dao = new StaffDAO();

        public void AddNewStaff(Staff staff) => dao.AddNewStaff(staff);

        public bool ChangeStatusStaff(Staff staff) => dao.ChangeStatusStaff(staff);

        public List<Staff> GetAllStaff() => dao.GetAllStaff();

        public Staff GetStaffByID(Guid id) => dao.GetStaffByID(id);

        public void UpdateStaff(Staff staff) => dao.UpdateStaff(staff);

    }
}
