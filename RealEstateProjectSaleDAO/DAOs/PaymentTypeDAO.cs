using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PaymentTypeDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public PaymentTypeDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<PaymentType> GetAllPaymentType()
        {
            try
            {
                return _context.PaymentTypes!.Include(c => c.Payments)
                                       .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewPaymentType(PaymentType type)
        {
            try
            {
                _context.Add(type);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PaymentType GetPaymentTypeByID(Guid id)
        {
            try
            {
                var type = _context.PaymentTypes!.Include(a => a.Payments)
                                           .SingleOrDefault(c => c.PaymentTypeID == id);
                return type;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePaymentType(PaymentType type)
        {
            try
            {
                var a = _context.PaymentTypes!.SingleOrDefault(c => c.PaymentTypeID == type.PaymentTypeID);

                _context.Entry(a).CurrentValues.SetValues(type);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeletePaymentTypeByID(Guid id)
        {
            var type = _context.PaymentTypes!.SingleOrDefault(lo => lo.PaymentTypeID == id);
            if (type != null)
            {
                _context.Remove(type);
                _context.SaveChanges();
            }
        }

    }
}
