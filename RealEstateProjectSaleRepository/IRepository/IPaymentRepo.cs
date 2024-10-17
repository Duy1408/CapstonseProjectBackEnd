using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPaymentRepo
    {
        List<Payment> GetAllPayment();
        Payment GetPaymentByID(Guid id);
        void AddNewPayment(Payment payment);
        void UpdatePayment(Payment payment);
        bool ChangeStatusPayment(Payment payment);


    }
}
