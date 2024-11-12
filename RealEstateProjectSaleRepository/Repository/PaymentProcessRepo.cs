using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class PaymentProcessRepo : IPaymentProcessRepo
    {
        private PaymentProcessDAO _dao;
        public PaymentProcessRepo()
        {
            _dao = new PaymentProcessDAO();
        }
        public void AddNew(PaymentProcess p)
        {
            _dao.AddNew(p);
        }

        public void DeletePaymentProcessByID(Guid id)
        {
            _dao.DeletePaymentProcessByID(id);
        }

        public List<PaymentProcess> GetPaymentProcess()
        {
            return _dao.GetAllPaymentProcess();
        }

        public PaymentProcess GetPaymentProcessById(Guid id)
        {
            return _dao.GetPaymentProcessByID(id);
        }

        public List<PaymentProcess> GetPaymentProcessBySalesPolicyID(Guid salesPolicyId)
        {
            return _dao.GetPaymentProcessBySalesPolicyID(salesPolicyId);
        }

        public void UpdatePaymentProcess(PaymentProcess p)
        {
            _dao.UpdatePaymentProcess(p);
        }
    }
}
