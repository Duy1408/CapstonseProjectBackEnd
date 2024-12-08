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
    public class PaymentProcessDetailRepo : IPaymentProcessDetailRepo
    {
        private PaymentProcessDetailDAO _dao;
        public PaymentProcessDetailRepo()
        {
            _dao = new PaymentProcessDetailDAO();
        }
        public void AddNew(PaymentProcessDetail p)
        {
            _dao.AddNew(p);
        }

        public PaymentProcessDetail CheckPaymentStage(Guid paymentProcessId, int paymentStage)
        {
            return _dao.CheckPaymentStage(paymentProcessId, paymentStage);
        }

        public void DeletePaymentProcessDetailByID(Guid id)
        {
            _dao.DeletePaymentProcessDetailByID(id);
        }

        public List<PaymentProcessDetail> GetPaymentProcessDetail()
        {
            return _dao.GetAllPaymentProcessDetail();
        }

        public PaymentProcessDetail GetPaymentProcessDetailById(Guid id)
        {
            return _dao.GetPaymentProcessDetailByID(id);
        }

        public List<PaymentProcessDetail> GetPaymentProcessDetailByPaymentProcessID(Guid pmtId)
        {
            return _dao.GetPaymentProcessDetailByPaymentProcessID(pmtId);
        }

        public float GetTotalPercentageByPaymentProcessID(Guid pmtId)
        {
            return _dao.GetTotalPercentageByPaymentProcessID(pmtId);
        }

        public void UpdatePaymentProcessDetail(PaymentProcessDetail p)
        {
            _dao.UpdatePaymentProcessDetail(p);
        }
    }
}
