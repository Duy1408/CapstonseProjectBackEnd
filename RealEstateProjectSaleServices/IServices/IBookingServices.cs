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

        Booking? GetBookingByDepositedTimed(Guid id);
        Booking? GetBookingByPropertyID(Guid propertyid);
        Booking? GetBookingByCustomerSelect(Guid customerId, Guid categoryDetailId);
        Booking? GetBookingByRandom(Guid id);

        List<Booking> GetBookingByBooked();
        List<Booking> GetBookingByCheckedIn(Guid openId);
        List<Booking> GetBookingByDocumentID(Guid id);
        List<Booking> GetBookingByCustomerID(Guid id);
        List<Booking> GetBookingByStaffID(Guid id);
        List<Booking> GetBookingByOpeningForSaleID(Guid id);

        Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID);
        string GenerateDocumentContent(Guid bookingId);

        bool ChangeStatusBooking(Booking booking);
    }
}
