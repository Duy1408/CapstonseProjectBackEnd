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
    public class StaffServices : IStaffServices
    {
        private readonly IStaffRepo _staffRepo;
        public StaffServices(IStaffRepo staffRepo)
        {
            _staffRepo = staffRepo;
        }
        public List<Staff> GetAllStaff() => _staffRepo.GetAllStaff();
        public void AddNewStaff(Staff staff) => _staffRepo.AddNewStaff(staff);

        public Staff GetStaffByID(Guid id) => _staffRepo.GetStaffByID(id);

        public void UpdateStaff(Staff staff) => _staffRepo.UpdateStaff(staff);

        public bool ChangeStatusStaff(Staff staff) => _staffRepo.ChangeStatusStaff(staff);

        public Staff GetStaffProfileByAccountID(Guid id)
        {
         return   _staffRepo.GetStaffProfileByAccountID(id);
        }
    }
}
