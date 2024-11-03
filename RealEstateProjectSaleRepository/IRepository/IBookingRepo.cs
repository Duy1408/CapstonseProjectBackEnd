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

        Booking? GetBookingByDepositedTimed(int numberBooking);

        Booking? GetBookingByRandom(int numberBooking);
        Booking? GetBookingByPropertyID(Guid propertyid);

        List<Booking> GetBookingByBooked();
        List<Booking> GetBookingByCheckedIn();
        List<Booking> GetBookingByDocumentID(Guid id);
        List<Booking> GetBookingByCustomerID(Guid id);
        Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID);



    }
}
