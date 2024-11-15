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
    public class PaymentPolicyRepo : IPaymentPolicyRepo
    {
        PaymentPolicyDAO dao = new PaymentPolicyDAO();

        public void AddNewPaymentPolicy(PaymentPolicy policy) => dao.AddNewPaymentPolicy(policy);

        public bool ChangeStatusPaymentPolicy(PaymentPolicy policy) => dao.ChangeStatusPaymentPolicy(policy);

        public List<PaymentPolicy> GetAllPaymentPolicy() => dao.GetAllPaymentPolicy();

        public PaymentPolicy GetPaymentPolicyByID(Guid id) => dao.GetPaymentPolicyByID(id);

        public void UpdatePaymentPolicy(PaymentPolicy policy) => dao.UpdatePaymentPolicy(policy);

    }
}
