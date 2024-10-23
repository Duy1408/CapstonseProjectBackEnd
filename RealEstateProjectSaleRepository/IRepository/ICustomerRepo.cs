using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface ICustomerRepo
    {
        List<Customer> GetAllCustomer();
        void AddNewCustomer(Customer customer);
        Customer GetCustomerByID(Guid id);
        Customer GetCustomerByAccountID(Guid id);
        void UpdateCustomer(Customer customer);
        bool ChangeStatusCustomer(Customer customer);

    }
}
