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
        bool ChangeStatus(Booking p);


        List<Booking> GetBookings();
        void AddNew(Booking p);


        Booking GetBookingById(Guid id);

        void UpdateBooking(Booking p);

        List<Booking> GetBookingByNumber(int numberBooking);
    }
}
