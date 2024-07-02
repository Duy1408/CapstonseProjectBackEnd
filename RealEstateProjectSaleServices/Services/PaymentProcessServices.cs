﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class PaymentProcessServices : IPaymentProcessServices
    {
        private IPaymentProcessRepo _repo;
        public PaymentProcessServices(IPaymentProcessRepo repo)
        {
            _repo = repo;
        }
        public void AddNew(PaymentProcess p)
        {
            _repo.AddNew(p);
        }

        public bool ChangeStatus(PaymentProcess p)
        {
            return _repo.ChangeStatus(p);
        }

        public List<PaymentProcess> GetPaymentProcess()
        {
            return _repo.GetPaymentProcess();
        }

        public PaymentProcess GetPaymentProcessById(Guid id)
        {
            return _repo.GetPaymentProcessById(id);
        }

        public void UpdatePaymentProcess(PaymentProcess p)
        {
            _repo.UpdatePaymentProcess(p);
        }
    }
}
