﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
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

        public bool ChangeStatus(PaymentProcess p)
        {
            return _dao.ChangeStatus(p);
        }

        public List<PaymentProcess> GetPaymentProcess()
        {
            return _dao.GetAllPaymentProcess();
        }

        public PaymentProcess GetPaymentProcessById(Guid id)
        {
            return _dao.GetPaymentProcessByID(id);
        }

        public void UpdatePaymentProcess(PaymentProcess p)
        {
            _dao.UpdatePaymentProcess(p);
        }
    }
}
