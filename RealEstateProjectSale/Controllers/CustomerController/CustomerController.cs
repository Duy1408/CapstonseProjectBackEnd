using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.CustomerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerServices _customerServices;
        private readonly IAccountServices _accountServices;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerServices customerServices, IAccountServices accountServices, IMapper mapper)
        {
            _customerServices = customerServices;
            _accountServices = accountServices;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            try
            {
                if (_customerServices.GetAllCustomer() == null)
                {
                    return NotFound();
                }
                var customers = _customerServices.GetAllCustomer();
                var response = _mapper.Map<List<CustomerVM>>(customers);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerByID(Guid id)
        {
            var customer = _customerServices.GetCustomerByID(id);

            if (customer != null)
            {
                var responese = _mapper.Map<CustomerVM>(customer);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddNewCustomer(RegisterCustomerVM accountCustomer)
        {
            try
            {
                var checkEmail = _accountServices.GetAllAccount().Where(u =>
                u.Email.Equals(accountCustomer.Email)).FirstOrDefault();

                if (checkEmail != null)
                {
                    return BadRequest("Email Existed");
                }

                var account = new AccountCreateDTO
                {
                    AccountID = Guid.NewGuid(),
                    Email = accountCustomer.Email,
                    Password = accountCustomer.Password,
                    Status = true,
                    RoleID = new Guid("4a09e2f3-8862-46e5-bef0-cce529a1a178")
                };

                var _account = _mapper.Map<Account>(account);
                _accountServices.AddNewAccount(_account);

                var customer = new CustomerCreateDTO
                {
                    CustomerID = Guid.NewGuid(),
                    FirstName = accountCustomer.FirstName,
                    LastName = accountCustomer.LastName,
                    DateOfBirth = accountCustomer.DateOfBirth,
                    PersonalEmail = accountCustomer.PersonalEmail,
                    PhoneNumber = accountCustomer.PhoneNumber,
                    IdentityCardNumber = accountCustomer.IdentityCardNumber,
                    Nationality = accountCustomer.Nationality,
                    Taxcode = accountCustomer.Taxcode,
                    BankName = accountCustomer.BankName,
                    BankNumber = accountCustomer.BankNumber,
                    Address = accountCustomer.Address,
                    Status = true,
                    AccountID = account.AccountID
                };

                var _customer = _mapper.Map<Customer>(customer);
                _customerServices.AddNewCustomer(_customer);

                return Ok("Create Customer Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(CustomerUpdateDTO customer, Guid id)
        {
            try
            {
                var existingCustomer = _customerServices.GetCustomerByID(id);
                if (existingCustomer != null)
                {
                    customer.CustomerID = existingCustomer.CustomerID;
                    customer.AccountID = existingCustomer.AccountID;

                    var _customer = _mapper.Map<Customer>(customer);
                    _customerServices.UpdateCustomer(_customer);

                    return Ok("Update Successfully");

                }

                return NotFound("Customer not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            if (_customerServices.GetCustomerByID(id) == null)
            {
                return NotFound();
            }
            var customer = _customerServices.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerServices.ChangeStatusCustomer(customer);


            return Ok("Delete Successfully");
        }

    }
}
