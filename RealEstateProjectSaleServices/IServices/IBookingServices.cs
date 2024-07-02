using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IBookingServices
    {
        public bool ChangeStatus(Booking p);


        public List<Booking> GetBookings();
        public void AddNew(Booking p);


        public Booking GetBookingById(Guid id);

        public void UpdateBooking(Booking p);
    }
}
