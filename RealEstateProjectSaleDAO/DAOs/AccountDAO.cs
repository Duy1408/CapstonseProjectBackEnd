using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class AccountDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public AccountDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public Account CheckLogin(string email, string password)
        {
            return _context.Accounts.Include(a => a.Role).Where(u => u.Email!.Equals(email) && u.Password!.Equals(password)).FirstOrDefault();

        }

        public List<Account> GetAllAccount()
        {
            try
            {
                return _context.Accounts!.Include(a => a.Role).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewAccount(Account account)
        {
            try
            {
                _context.Add(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Account GetAccountByID(Guid id)
        {
            try
            {
                var account = _context.Accounts!.Include(a => a.Role)
                                               .SingleOrDefault(c => c.AccountID == id);
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateAccount(Account account)
        {
            try
            {
                var a = _context.Accounts!.SingleOrDefault(c => c.AccountID == account.AccountID);

                _context.Entry(a).CurrentValues.SetValues(account);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusAccount(Account account)
        {
            var _account = _context.Accounts!.FirstOrDefault(c => c.AccountID.Equals(account.AccountID));


            if (_account == null)
            {
                return false;
            }
            else
            {
                _account.Status = false;
                _context.Entry(_account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
