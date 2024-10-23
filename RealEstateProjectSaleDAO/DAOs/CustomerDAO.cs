using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class CustomerDAO
    {

        private readonly RealEstateProjectSaleSystemDBContext _context;
        public CustomerDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<Customer> GetAllCustomer()
        {
            try
            {
                return _context.Customers!.Include(c => c.Account)
                                       .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewCustomer(Customer customer)
        {
            try
            {
                _context.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer GetCustomerByID(Guid id)
        {
            try
            {
                var customer = _context.Customers!.Include(a => a.Account)
                                           .SingleOrDefault(c => c.CustomerID == id);
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer GetCustomerByAccountID(Guid id)
        {
            try
            {
                var customer = _context.Customers!.Include(a => a.Account)
                                           .SingleOrDefault(c => c.AccountID == id);
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                var a = _context.Customers!.SingleOrDefault(c => c.CustomerID == customer.CustomerID);

                _context.Entry(a).CurrentValues.SetValues(customer);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusCustomer(Customer customer)
        {
            var _customer = _context.Customers!.FirstOrDefault(c => c.CustomerID.Equals(customer.CustomerID));


            if (_customer == null)
            {
                return false;
            }
            else
            {
                _customer.Status = false;
                _context.Entry(_customer).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
