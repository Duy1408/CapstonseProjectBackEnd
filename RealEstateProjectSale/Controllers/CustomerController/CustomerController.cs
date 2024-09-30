using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IRoleServices _roleServices;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerServices customerServices, IAccountServices accountServices,
                                  IMapper mapper, IRoleServices roleServices)
        {
            _customerServices = customerServices;
            _accountServices = accountServices;
            _roleServices = roleServices;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllCustomer")]
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

        [HttpGet("GetCustomerByID/{id}")]
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
        [Route("RegisterAccountCustomer")]
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

                var roleCustomer = _roleServices.GetRoleByRoleName("Customer");

                var account = new AccountCreateDTO
                {
                    AccountID = Guid.NewGuid(),
                    Email = accountCustomer.Email,
                    Password = accountCustomer.Password,
                    Status = true,
                    RoleID = roleCustomer.RoleID
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

        [HttpPut("UpdateCustomer/{id}")]
        public IActionResult UpdateCustomer([FromForm] CustomerUpdateDTO customer, Guid id)
        {
            try
            {
                var existingCustomer = _customerServices.GetCustomerByID(id);
                if (existingCustomer != null)
                {

                    if (!string.IsNullOrEmpty(customer.FirstName))
                    {
                        existingCustomer.FirstName = customer.FirstName;
                    }
                    if (!string.IsNullOrEmpty(customer.LastName))
                    {
                        existingCustomer.LastName = customer.LastName;
                    }
                    if (customer.DateOfBirth.HasValue)
                    {
                        existingCustomer.DateOfBirth = customer.DateOfBirth.Value;
                    }
                    //if (!string.IsNullOrEmpty(customer.PersonalEmail))
                    //{
                    //    existingCustomer.PersonalEmail = customer.PersonalEmail;
                    //}
                    if (!string.IsNullOrEmpty(customer.PhoneNumber))
                    {
                        existingCustomer.PhoneNumber = customer.PhoneNumber;
                    }
                    if (!string.IsNullOrEmpty(customer.IdentityCardNumber))
                    {
                        existingCustomer.IdentityCardNumber = customer.IdentityCardNumber;
                    }
                    if (!string.IsNullOrEmpty(customer.Nationality))
                    {
                        existingCustomer.Nationality = customer.Nationality;
                    }
                    if (!string.IsNullOrEmpty(customer.Taxcode))
                    {
                        existingCustomer.Taxcode = customer.Taxcode;
                    }
                    if (!string.IsNullOrEmpty(customer.BankName))
                    {
                        existingCustomer.BankName = customer.BankName;
                    }
                    if (customer.BankNumber.HasValue)
                    {
                        existingCustomer.BankNumber = customer.BankNumber.Value;
                    }
                    if (!string.IsNullOrEmpty(customer.Address))
                    {
                        existingCustomer.Address = customer.Address;
                    }
                    if (customer.Status.HasValue)
                    {
                        existingCustomer.Status = customer.Status.Value;
                    }

                    //var _customer = _mapper.Map<Customer>(customer);
                    _customerServices.UpdateCustomer(existingCustomer);

                    return Ok("Update Customer Successfully");

                }

                return NotFound("Customer not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteCustomer/{id}")]
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
