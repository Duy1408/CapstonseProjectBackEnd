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

        public List<Booking> GetBookingByBooked()
        {
            return dao.GetBookingByBooked();
        }

        public List<Booking> GetBookingByCheckedIn()
        {
            return dao.GetBookingByCheckedIn();
        }

        public List<Booking> GetBookingByDepositedTimed(int numberBooking)
        {
            return dao.GetBookingByDepositedTimed(numberBooking);
        }

        public Booking GetBookingByDocumentID(Guid id)
        {
            return dao.GetBookingByDocumentID(id);
        }

        public Booking GetBookingById(Guid id)
        {
            return dao.GetBookingByID(id);
        }

        public List<Booking> GetBookingByRandom(int numberBooking)
        {
            return dao.GetBookingByRandom(numberBooking);
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
