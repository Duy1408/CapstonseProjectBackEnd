using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ContractPaymentDetailDAO
    {
        private static ContractPaymentDetailDAO instance;

        public static ContractPaymentDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContractPaymentDetailDAO();
                }
                return instance;
            }


        }

        public List<ContractPaymentDetail> GetAllBookingPaymentProcessDetail()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.ContractPaymentDetails.ToList();
        }

        public bool AddNew(ContractPaymentDetail p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.ContractPaymentDetails.SingleOrDefault(c => c.ContractPaymentDetailID == p.ContractPaymentDetailID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.ContractPaymentDetails.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool Update(ContractPaymentDetail p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.ContractPaymentDetails.SingleOrDefault(c => c.ContractPaymentDetailID == p.ContractPaymentDetailID);

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

        public bool ChangeStatus(ContractPaymentDetail p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.ContractPaymentDetails.FirstOrDefault(c => c.ContractPaymentDetailID.Equals(p.ContractPaymentDetailID));


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
        public ContractPaymentDetail GetBookingPaymentProcessDetailByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.ContractPaymentDetails.SingleOrDefault(a => a.ContractPaymentDetailID == id);
        }
    }
}
