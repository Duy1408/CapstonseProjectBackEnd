using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IBookingRepo
    {

        public List<Booking> GetBookings();
        public void AddNew(Booking p);


        public Booking GetBookingById(Guid id);

        public void UpdateBooking(Booking p);

        Booking? GetBookingByDepositedTimed(Guid id);

        Booking? GetBookingByRandom(Guid id);
        Booking? GetBookingByPropertyID(Guid propertyid);
        Booking? GetBookingByCustomerSelect(Guid id);

        List<Booking> GetBookingByBooked();
        List<Booking> GetBookingByCheckedIn(Guid openId);
        List<Booking> GetBookingByDocumentID(Guid id);
        List<Booking> GetBookingByCustomerID(Guid id);
        List<Booking> GetBookingByStaffID(Guid id);
        List<Booking> GetBookingByOpeningForSaleID(Guid id);
        Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID);

        bool ChangeStatusBooking(Booking booking);

    }
}
