﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IAccountServices
    {
        List<Account> GetAllAccount();
        void AddNewAccount(Account account);
        Account GetAccountByID(Guid id);
        void UpdateAccount(Account account);
        bool ChangeStatusAccount(Account account);
        Account CheckLogin(string email, string password);
    }
}
