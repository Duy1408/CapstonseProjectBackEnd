using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PaymentProcessDetailDAO
    {
        private static PaymentProcessDetailDAO instance;

        public static PaymentProcessDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PaymentProcessDetailDAO();
                }
                return instance;
            }


        }

        public PaymentProcessDetail CheckPaymentStage(Guid paymentProcessId, int paymentStage)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();

            return _context.PaymentProcessDetails.Include(p => p.PaymentProcess)
                                                 .FirstOrDefault(a => a.PaymentProcessID == paymentProcessId
                                                        && a.PaymentStage == paymentStage);

        }

        public List<PaymentProcessDetail> GetAllPaymentProcessDetail()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PaymentProcessDetails.Include(p => p.PaymentProcess).ToList();
        }

        public bool AddNew(PaymentProcessDetail p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PaymentProcessDetails.SingleOrDefault(c => c.PaymentProcessDetailID == p.PaymentProcessDetailID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.PaymentProcessDetails.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdatePaymentProcessDetail(PaymentProcessDetail p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PaymentProcessDetails.SingleOrDefault(c => c.PaymentProcessDetailID == p.PaymentProcessDetailID);

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

        public void DeletePaymentProcessDetailByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var detail = _context.PaymentProcessDetails!.SingleOrDefault(lo => lo.PaymentProcessDetailID == id);
            if (detail != null)
            {
                _context.Remove(detail);
                _context.SaveChanges();
            }
        }

        public PaymentProcessDetail GetPaymentProcessDetailByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PaymentProcessDetails.Include(p => p.PaymentProcess).SingleOrDefault(a => a.PaymentProcessDetailID == id);
        }

        public List<PaymentProcessDetail> GetPaymentProcessDetailByPaymentProcessID(Guid pmtId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PaymentProcessDetails.Include(c => c.PaymentProcess)
                                         .Where(a => a.PaymentProcessID == pmtId)
                                         .ToList();
        }

        public float GetTotalPercentageByPaymentProcessID(Guid pmtId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();

            var totalPercentage = _context.PaymentProcessDetails
                                          .Where(a => a.PaymentProcessID == pmtId)
                                          .Sum(a => a.Percentage) ?? 0;

            return totalPercentage;
        }

    }
}
