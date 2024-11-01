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

        List<Booking> GetBookings();
        void AddNew(Booking p);


        Booking GetBookingById(Guid id);

        void UpdateBooking(Booking p);

        Booking? GetBookingByDepositedTimed(int numberBooking);

        Booking? GetBookingByRandom(int numberBooking);

        List<Booking> GetBookingByBooked();
        List<Booking> GetBookingByCheckedIn();
        List<Booking> GetBookingByDocumentID(Guid id);
        List<Booking> GetBookingByCustomerID(Guid id);
        Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID);
        string GenerateDocumentContent(Guid bookingId);

    }
}
