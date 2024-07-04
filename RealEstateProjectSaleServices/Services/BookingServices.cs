using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _book;
        public BookingServices(IBookingRepo book)
        {
            _book = book;
        }
        public void AddNew(Booking p)
        {
            _book.AddNew(p);
        }

        public bool ChangeStatus(Booking p)
        {
            return _book.ChangeStatus(p);
        }

        public List<Booking> GetBookingByNumber(int numberBooking)
        {
            return _book.GetBookingByNumber(numberBooking);
        }

        public Booking GetBookingById(Guid id)
        {
            return _book.GetBookingById(id);
        }

        public List<Booking> GetBookings()
        {
            return _book.GetBookings();
        }

        public void UpdateBooking(Booking p)
        {
            _book.UpdateBooking(p);
        }
    }
}
