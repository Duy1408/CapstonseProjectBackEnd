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
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepo _accountRepo;
        public AccountServices(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public void AddNewAccount(Account account) => _accountRepo.AddNewAccount(account);

        public bool ChangeStatusAccount(Account account) => _accountRepo.ChangeStatusAccount(account);

        public Account CheckEmailOrPhone(string email) => _accountRepo.CheckEmailOrPhone(email);

        public Account CheckLogin(string email, string password) => _accountRepo.CheckLogin(email, password);

        public Account GetAccountByID(Guid id) => _accountRepo.GetAccountByID(id);

        public List<Account> GetAllAccount() => _accountRepo.GetAllAccount();

        public void UpdateAccount(Account account) => _accountRepo.UpdateAccount(account);
    }

}
