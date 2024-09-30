using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Enums;
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
            return _context.Bookings
                                    .Include(c => c.OpeningForSale)
                                    //.Include(c => c.Project)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .ToList();
        }

        public List<Booking> GetBookingByBooked()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings
                                    .Include(c => c.OpeningForSale)
                                    //.Include(c => c.Project)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Where(b => b.Status == BookingStatus.Reserved.ToString())
                                    .ToList();
        }

        public List<Booking> GetBookingByCheckedIn()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!
                                    .Include(c => c.OpeningForSale)
                                    //.Include(c => c.Project)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Where(b => b.Status == BookingStatus.CheckedIn.ToString())
                                    .ToList();
        }

        public List<Booking> GetBookingByDepositedTimed(int numberBooking)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!
                                     .Include(c => c.OpeningForSale)
                                     //.Include(c => c.Project)
                                     .Include(c => c.Customer)
                                     .Include(c => c.Staff)
                                     .Where(b => b.DepositedTimed != null
                                     && b.DepositedPrice != null
                                     && b.Status != BookingStatus.ContractSigned.ToString()
                                     && b.Status == BookingStatus.CheckedIn.ToString())
                                     .OrderBy(b => b.DepositedTimed)
                                     .ThenBy(b => b.BookingID)
                                     .Take(numberBooking)
                                     .ToList();
        }

        public List<Booking> GetBookingByRandom(int numberBooking)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!
                                     .Include(c => c.OpeningForSale)
                                     //.Include(c => c.Project)
                                     .Include(c => c.Customer)
                                     .Include(c => c.Staff)
                                     .Where(b => b.DepositedTimed != null
                                     && b.DepositedPrice != null
                                     && b.Status != BookingStatus.ContractSigned.ToString()
                                     && b.Status == BookingStatus.CheckedIn.ToString())
                                     .OrderBy(b => Guid.NewGuid())
                                     .Take(numberBooking)
                                     .ToList();
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

        public Booking GetBookingByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings
                                    .Include(c => c.OpeningForSale)
                                    //.Include(c => c.Project)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .SingleOrDefault(a => a.BookingID == id);
        }

    }
}
