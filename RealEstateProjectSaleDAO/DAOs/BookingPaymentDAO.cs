using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class BookingPaymentDAO

    {
        private static BookingPaymentDAO instance;

        public static BookingPaymentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingPaymentDAO();
                }
                return instance;
            }


        }

        public List<Payment> GetAllBookingPayment()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Payments.ToList();
        }

        public bool AddNew(Payment p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Payments.SingleOrDefault(c => c.PaymentID == p.PaymentID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Payments.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdateBookingPayment(Payment p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Payments.SingleOrDefault(c => c.PaymentID == p.PaymentID);

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

        public bool ChangeStatus(Payment p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Payments.FirstOrDefault(c => c.PaymentID.Equals(p.PaymentID));


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

        public Payment GetBookingPaymentByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Payments.SingleOrDefault(a => a.PaymentID == id);
        }

    }
}
