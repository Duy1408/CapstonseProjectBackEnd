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

        public bool ChangeStatus(Booking p)
        {
            return dao.ChangeStatus(p);
        }

        public Booking GetBookingById(Guid id)
        {
            return dao.GetBookingByID(id);
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
