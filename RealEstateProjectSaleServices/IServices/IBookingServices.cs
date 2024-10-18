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

        List<Booking> GetBookingByDepositedTimed(int numberBooking);

        List<Booking> GetBookingByRandom(int numberBooking);

        List<Booking> GetBookingByBooked();
        List<Booking> GetBookingByCheckedIn();
        Booking GetBookingByDocumentID(Guid id);

        string GenerateDocumentContent(Guid templateId);

    }
}
