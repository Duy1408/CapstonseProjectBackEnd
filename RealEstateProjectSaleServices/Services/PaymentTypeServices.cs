using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class PaymentTypeServices : IPaymentTypeServices
    {
        private readonly IPaymentTypeRepo _typeRepo;
        public PaymentTypeServices(IPaymentTypeRepo typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public void AddNewPaymentType(PaymentType type) => _typeRepo.AddNewPaymentType(type);

        public void DeletePaymentTypeByID(Guid id) => _typeRepo?.DeletePaymentTypeByID(id);

        public List<PaymentType> GetAllPaymentType() => _typeRepo.GetAllPaymentType();

        public PaymentType GetPaymentTypeByID(Guid id) => _typeRepo.GetPaymentTypeByID(id);

        public void UpdatePaymentType(PaymentType type) => _typeRepo.UpdatePaymentType(type);

    }
}
