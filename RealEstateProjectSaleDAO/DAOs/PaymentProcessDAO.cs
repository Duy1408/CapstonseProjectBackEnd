using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PaymentProcessDAO
    {
        private static PaymentProcessDAO instance;

        public static PaymentProcessDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PaymentProcessDAO();
                }
                return instance;
            }


        }

        public List<PaymentProcess> GetAllPaymentProcess()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PaymentProcesses.Include(p => p.Salespolicy).ToList();
        }

        public bool AddNew(PaymentProcess p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PaymentProcesses.SingleOrDefault(c => c.PaymentProcessID == p.PaymentProcessID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.PaymentProcesses.Add(p);
                _context.SaveChanges();
                return true;

            }
        }


        public bool UpdatePaymentProcess(PaymentProcess p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PaymentProcesses.SingleOrDefault(c => c.PaymentProcessID == p.PaymentProcessID);

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

        public void DeletePaymentProcessByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var process = _context.PaymentProcesses!.SingleOrDefault(lo => lo.PaymentProcessID == id);
            if (process != null)
            {
                _context.Remove(process);
                _context.SaveChanges();
            }
        }

        public PaymentProcess GetPaymentProcessByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PaymentProcesses.Include(p => p.Salespolicy).SingleOrDefault(a => a.PaymentProcessID == id);
        }
    }
}
