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
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepo _customerRepo;
        public CustomerServices(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public void AddNewCustomer(Customer customer) => _customerRepo.AddNewCustomer(customer);

        public bool ChangeStatusCustomer(Customer customer) => _customerRepo.ChangeStatusCustomer(customer);

        public Customer CheckCustomerByIdentification(Guid id) => _customerRepo.CheckCustomerByIdentification(id);

        public List<Customer> GetAllCustomer() => _customerRepo.GetAllCustomer();

        public Customer GetCustomerByAccountID(Guid id) => _customerRepo.GetCustomerByAccountID(id);

        public Customer GetCustomerByID(Guid id) => _customerRepo.GetCustomerByID(id);

        public void UpdateCustomer(Customer customer) => _customerRepo.UpdateCustomer(customer);

    }
}
