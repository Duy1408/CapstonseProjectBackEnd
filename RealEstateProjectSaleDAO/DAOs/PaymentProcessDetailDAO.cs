﻿using Microsoft.EntityFrameworkCore;
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
    }
}
