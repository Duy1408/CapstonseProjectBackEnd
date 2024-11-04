using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface ICustomerServices
    {
        List<Customer> GetAllCustomer();
        void AddNewCustomer(Customer customer);
        Customer GetCustomerByID(Guid id);
        Customer GetCustomerByAccountID(Guid id);
        void UpdateCustomer(Customer customer);
        bool ChangeStatusCustomer(Customer customer);
        Customer CheckCustomerByIdentification(Guid id);

    }
}
