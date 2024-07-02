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
    public class PaymentTypeRepo : IPaymentTypeRepo
    {
        PaymentTypeDAO dao = new PaymentTypeDAO();

        public void AddNewPaymentType(PaymentType type) => dao.AddNewPaymentType(type);

        public void DeletePaymentTypeByID(Guid id) => dao.DeletePaymentTypeByID(id);

        public List<PaymentType> GetAllPaymentType() => dao.GetAllPaymentType();

        public PaymentType GetPaymentTypeByID(Guid id) => dao.GetPaymentTypeByID(id);

        public void UpdatePaymentType(PaymentType type) => dao.UpdatePaymentType(type);

    }
}
