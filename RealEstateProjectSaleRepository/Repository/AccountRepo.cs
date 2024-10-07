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
    public class AccountRepo : IAccountRepo
    {
        AccountDAO dao = new AccountDAO();
        public void AddNewAccount(Account account) => dao.AddNewAccount(account);

        public bool ChangeStatusAccount(Account account) => dao.ChangeStatusAccount(account);

        public Account CheckEmailOrPhone(string email) => dao.CheckEmailOrPhone(email);

        public Account CheckLogin(string email, string password) => dao.CheckLogin(email, password);

        public Account GetAccountByID(Guid id) => dao.GetAccountByID(id);

        public List<Account> GetAllAccount() => dao.GetAllAccount();

        public void UpdateAccount(Account account) => dao.UpdateAccount(account);

    }
}
