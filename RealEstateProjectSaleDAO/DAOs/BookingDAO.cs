using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class BookingDAO
    {
        private static BookingDAO instance;

        public static BookingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingDAO();
                }
                return instance;
            }


        }

        public List<Booking> GetAllBooking()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.ToList();
        }

        public List<Booking> GetBookingByNumber(int numberBooking)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.OrderBy(b=>b.BookingID).Take(numberBooking).ToList();
        }

        public bool AddNew(Booking p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Bookings.SingleOrDefault(c => c.BookingID == p.BookingID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Bookings.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdateBooking(Booking p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Bookings.SingleOrDefault(c => c.BookingID == p.BookingID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(p);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(Booking p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Bookings.FirstOrDefault(c => c.BookingID.Equals(p.BookingID));


            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

        public Booking GetBookingByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.SingleOrDefault(a => a.BookingID == id);
        }

    }
}
