using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPaymentTypeRepo
    {
        List<PaymentType> GetAllPaymentType();
        void AddNewPaymentType(PaymentType type);
        PaymentType GetPaymentTypeByID(Guid id);
        void UpdatePaymentType(PaymentType type);
        void DeletePaymentTypeByID(Guid id);

    }
}
