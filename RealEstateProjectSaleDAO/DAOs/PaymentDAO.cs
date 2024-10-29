using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PaymentDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public PaymentDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<Payment> GetAllPayment()
        {
            try
            {
                return _context.Payments!.Include(a => a.Customer)
                                        
                                         .Include(a => a.Booking)
                                         .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewPayment(Payment payment)
        {
            try
            {
                _context.Add(payment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Payment GetPaymentByID(Guid id)
        {
            try
            {
                var payment = _context.Payments!.Include(a => a.Customer)
                                                
                                                .Include(a => a.Booking)
                                                .SingleOrDefault(c => c.PaymentID == id);
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePayment(Payment payment)
        {
            try
            {
                var a = _context.Payments!.SingleOrDefault(c => c.PaymentID == payment.PaymentID);

                _context.Entry(a).CurrentValues.SetValues(payment);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusPayment(Payment payment)
        {
            var _payment = _context.Payments!.FirstOrDefault(c => c.PaymentID.Equals(payment.PaymentID));


            if (_payment == null)
            {
                return false;
            }
            else
            {
                _payment.Status = false;
                _context.Entry(_payment).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
