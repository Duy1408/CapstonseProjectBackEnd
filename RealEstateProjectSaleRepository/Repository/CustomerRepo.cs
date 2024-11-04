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
    public class CustomerRepo : ICustomerRepo
    {
        CustomerDAO dao = new CustomerDAO();

        public void AddNewCustomer(Customer customer) => dao.AddNewCustomer(customer);

        public bool ChangeStatusCustomer(Customer customer) => dao.ChangeStatusCustomer(customer);

        public Customer CheckCustomerByIdentification(Guid id) => dao.CheckCustomerByIdentification(id);

        public List<Customer> GetAllCustomer() => dao.GetAllCustomer();

        public Customer GetCustomerByAccountID(Guid id) => dao.GetCustomerByAccountID(id);

        public Customer GetCustomerByID(Guid id) => dao.GetCustomerByID(id);

        public void UpdateCustomer(Customer customer) => dao.UpdateCustomer(customer);

    }
}
