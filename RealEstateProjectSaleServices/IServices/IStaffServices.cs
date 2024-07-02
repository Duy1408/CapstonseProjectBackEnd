using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IStaffServices
    {
        List<Staff> GetAllStaff();
        void AddNewStaff(Staff staff);
        Staff GetStaffByID(Guid id);
        void UpdateStaff(Staff staff);
        bool ChangeStatusStaff(Staff staff);
    }
}
