using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
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
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .ToList();
        }

        public List<Booking> GetBookingByBooked()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(b => b.Status == BookingStatus.DaDatCho.GetEnumDescription())
                                    .ToList();
        }

        public List<Booking> GetBookingByCheckedIn(Guid openId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(b => b.OpeningForSaleID == openId
                                    && b.Status == BookingStatus.DaCheckIn.GetEnumDescription()
                                    && b.DepositedTimed != null)
                                    .OrderBy(b => b.DepositedTimed)
                                    .ThenBy(b => b.BookingID)
                                    .ToList();
        }

        public Booking? GetBookingByDepositedTimed(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!.Include(c => c.OpeningForSale)
                                     .Include(c => c.Customer)
                                     .Include(c => c.Staff)
                                     .Include(c => c.Property)
                                     .Include(c => c.DocumentTemplate)
                                     .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                     .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                     .Where(b => b.DepositedTimed != null
                                     && b.DepositedPrice != null
                                     && b.Status == BookingStatus.DaCheckIn.GetEnumDescription()
                                     && b.ProjectCategoryDetailID == id)
                                     .OrderBy(b => b.DepositedTimed)
                                     .ThenBy(b => b.BookingID)
                                     .FirstOrDefault();
        }

        public Booking? GetBookingByPropertyID(Guid propertyid)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!.Include(c => c.OpeningForSale)
                                     .Include(c => c.Customer)
                                     .Include(c => c.Staff)
                                     .Include(c => c.Property)
                                     .Include(c => c.DocumentTemplate)
                                     .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                     .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                     .Where(b => b.PropertyID == propertyid
                                     && b.Status != BookingStatus.DaHuy.GetEnumDescription())
                                                                    .FirstOrDefault();
        }

        public Booking? GetBookingByRandom(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!.Include(c => c.OpeningForSale)
                                     .Include(c => c.Customer)
                                     .Include(c => c.Staff)
                                     .Include(c => c.Property)
                                     .Include(c => c.DocumentTemplate)
                                     .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                     .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                     .Where(b => b.DepositedTimed != null
                                     && b.DepositedPrice != null
                                     && b.Status == BookingStatus.DaCheckIn.GetEnumDescription()
                                     && b.ProjectCategoryDetailID == id)
                                     .OrderBy(b => Guid.NewGuid())
                                     .FirstOrDefault();
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
            return _context.Bookings.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .SingleOrDefault(a => a.BookingID == id);
        }

        public List<Booking> GetBookingByDocumentID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(a => a.DocumentTemplateID == id)
                                    .ToList();
        }

        public List<Booking> GetBookingByCustomerID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(a => a.CustomerID == id)
                                    .ToList();
        }

        public List<Booking> GetBookingByCategoryDetailID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(a => a.ProjectCategoryDetailID == id)
                                    .ToList();
        }

        public List<Booking> GetBookingByOpeningForSaleID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(a => a.OpeningForSaleID == id)
                                    .ToList();
        }

        public List<Booking> GetBookingByStaffID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(a => a.StaffID == id)
                                    .ToList();
        }

        public Booking? GetBookingByCustomerSelect(Guid customerId, Guid categoryDetailId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings!.Include(c => c.OpeningForSale)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Staff)
                                    .Include(c => c.Property)
                                    .Include(c => c.DocumentTemplate)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.Project)
                                    .Include(o => o.ProjectCategoryDetail)
                                        .ThenInclude(pc => pc.PropertyCategory)
                                    .Where(b => b.CustomerID == customerId
                                     && b.ProjectCategoryDetailID == categoryDetailId
                                     && b.Status == BookingStatus.DaCheckIn.GetEnumDescription())
                                                                    .FirstOrDefault();
        }

        public Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Bookings.FirstOrDefault(b => b.OpeningForSaleID == openForSaleID
                                  && b.ProjectCategoryDetailID == categoryDetailID
                                  && b.CustomerID == customerID);
        }

        public bool ChangeStatusBooking(Booking booking)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var _booking = _context.Bookings!.FirstOrDefault(c => c.BookingID.Equals(booking.BookingID));


            if (_booking == null)
            {
                return false;
            }
            else
            {
                _booking.Status = BookingStatus.DaHuy.GetEnumDescription();
                _context.Entry(_booking).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
