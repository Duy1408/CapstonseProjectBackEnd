using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PaymentPolicyDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public PaymentPolicyDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<PaymentPolicy> GetAllPaymentPolicy()
        {
            try
            {
                return _context.PaymentPolicys!.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewPaymentPolicy(PaymentPolicy policy)
        {
            try
            {
                _context.Add(policy);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public PaymentPolicy GetPaymentPolicyByID(Guid id)
        {
            try
            {
                var policy = _context.PaymentPolicys!.SingleOrDefault(c => c.PaymentPolicyID == id);
                return policy;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePaymentPolicy(PaymentPolicy policy)
        {
            try
            {
                var a = _context.PaymentPolicys!.SingleOrDefault(c => c.PaymentPolicyID == policy.PaymentPolicyID);

                _context.Entry(a).CurrentValues.SetValues(policy);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusPaymentPolicy(PaymentPolicy policy)
        {
            var _policy = _context.PaymentPolicys!.FirstOrDefault(c => c.PaymentPolicyID.Equals(policy.PaymentPolicyID));


            if (_policy == null)
            {
                return false;
            }
            else
            {
                _policy.Status = false;
                _context.Entry(_policy).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
