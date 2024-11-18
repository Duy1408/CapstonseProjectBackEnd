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

        public bool ChangeStatusBooking(Booking booking)
        {
            return dao.ChangeStatusBooking(booking);
        }

        public Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID)
        {
            return dao.CheckExistingBooking(openForSaleID, categoryDetailID, customerID);
        }

        public List<Booking> GetBookingByBooked()
        {
            return dao.GetBookingByBooked();
        }

        public List<Booking> GetBookingByCheckedIn(Guid openId)
        {
            return dao.GetBookingByCheckedIn(openId);
        }

        public List<Booking> GetBookingByCustomerID(Guid id)
        {
            return dao.GetBookingByCustomerID(id);
        }

        public Booking? GetBookingByCustomerSelect(Guid customerId, Guid categoryDetailId)
        {
            return dao.GetBookingByCustomerSelect(customerId, categoryDetailId);
        }

        public Booking? GetBookingByDepositedTimed(Guid id)
        {
            return dao.GetBookingByDepositedTimed(id);
        }

        public List<Booking> GetBookingByDocumentID(Guid id)
        {
            return dao.GetBookingByDocumentID(id);
        }

        public Booking GetBookingById(Guid id)
        {
            return dao.GetBookingByID(id);
        }

        public List<Booking> GetBookingByOpeningForSaleID(Guid id)
        {
            return dao.GetBookingByOpeningForSaleID(id);
        }

        public Booking? GetBookingByPropertyID(Guid propertyid)
        {
            return dao.GetBookingByPropertyID(propertyid);
        }

        public Booking? GetBookingByRandom(Guid id)
        {
            return dao.GetBookingByRandom(id);
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
