﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPaymentProcessDetailRepo
    {
        public bool ChangeStatus(PaymentProcessDetail p);


        public List<PaymentProcessDetail> GetPaymentProcessDetail();
        public void AddNew(PaymentProcessDetail p);


        public PaymentProcessDetail GetPaymentProcessDetailById(Guid id);

        public void UpdatePaymentProcessDetail(PaymentProcessDetail p);
    }
}
