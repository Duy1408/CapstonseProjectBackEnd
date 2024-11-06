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
    public class BookingRepo : IBookingRepo
    {
        private readonly BookingDAO dao;
        public BookingRepo()
        {
            dao = new BookingDAO();
        }
        public void AddNew(Booking p)
        {
            dao.AddNew(p);
        }

        public Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID)
        {
            return dao.CheckExistingBooking(openForSaleID, categoryDetailID, customerID);
        }

        public List<Booking> GetBookingByBooked()
        {
            return dao.GetBookingByBooked();
        }

        public List<Booking> GetBookingByCheckedIn()
        {
            return dao.GetBookingByCheckedIn();
        }

        public List<Booking> GetBookingByCustomerID(Guid id)
        {
            return dao.GetBookingByCustomerID(id);
        }

        public Booking? GetBookingByCustomerSelect(Guid id)
        {
            return dao.GetBookingByCustomerSelect(id);
        }

        public Booking? GetBookingByDepositedTimed()
        {
            return dao.GetBookingByDepositedTimed();
        }

        public List<Booking> GetBookingByDocumentID(Guid id)
        {
            return dao.GetBookingByDocumentID(id);
        }

        public Booking GetBookingById(Guid id)
        {
            return dao.GetBookingByID(id);
        }

        public Booking? GetBookingByPropertyID(Guid propertyid)
        {
            return dao.GetBookingByPropertyID(propertyid);
        }

        public Booking? GetBookingByRandom()
        {
            return dao.GetBookingByRandom();
        }

        public List<Booking> GetBookingByStaffID(Guid id)
        {
            return dao.GetBookingByStaffID(id);
        }

        public List<Booking> GetBookings()
        {
            return dao.GetAllBooking();
        }

        public void UpdateBooking(Booking p)
        {
            dao.UpdateBooking(p);
        }
    }
}
