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
    public class PaymentRepo : IPaymentRepo
    {
        PaymentDAO dao = new PaymentDAO();

        public void AddNewPayment(Payment payment) => dao.AddNewPayment(payment);

        public bool ChangeStatusPayment(Payment payment) => dao.ChangeStatusPayment(payment);

        public List<Payment> GetAllPayment() => dao.GetAllPayment();

        public Payment GetPaymentByID(Guid id) => dao.GetPaymentByID(id);

        public void UpdatePayment(Payment payment) => dao.UpdatePayment(payment);

    }
}
