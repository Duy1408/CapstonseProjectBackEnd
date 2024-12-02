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
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.CustomerController
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerServices _customerServices;
        private readonly IAccountServices _accountServices;
        private readonly IJWTTokenService _jWTTokenService;
        private readonly IRoleServices _roleServices;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerServices customerServices, IAccountServices accountServices,
                                  IMapper mapper, IRoleServices roleServices, IJWTTokenService jWTTokenService)
        {
            _customerServices = customerServices;
            _accountServices = accountServices;
            _jWTTokenService = jWTTokenService;
            _roleServices = roleServices;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "GetAllCustomer")]
        public IActionResult GetAllCustomer()
        {
            try
            {
                if (_customerServices.GetAllCustomer() == null)
                {
                    return NotFound(new
                    {
                        message = "Khách hàng không tồn tại."
                    });
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
        [SwaggerOperation(Summary = "GetCustomerByID")]
        public IActionResult GetCustomerByID(Guid id)
        {
            var customer = _customerServices.GetCustomerByID(id);

            if (customer != null)
            {
                var responese = _mapper.Map<CustomerVM>(customer);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Khách hàng không tồn tại."
            });

        }

        [HttpGet("account/{accountId}")]
        [SwaggerOperation(Summary = "Get Customer By AccountID")]
        public IActionResult GetCustomerByAccountID(Guid accountId)
        {
            var customer = _customerServices.GetCustomerByAccountID(accountId);

            if (customer != null)
            {
                var responese = _mapper.Map<CustomerVM>(customer);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Khách hàng không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Register Account Customer")]
        public async Task<IActionResult> AddNewCustomer(RegisterCustomerVM accountCustomer)
        {
            try
            {
                var checkEmail = _accountServices.GetAllAccount().Where(u =>
                u.Email.Equals(accountCustomer.Email) && u.Status == true).FirstOrDefault();

                if (checkEmail != null)
                {
                    return BadRequest(new
                    {
                        message = "Email tồn tại"
                    });
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
                    FullName = accountCustomer.FullName,
                    DateOfBirth = accountCustomer.DateOfBirth!.Value,
                    PhoneNumber = accountCustomer.PhoneNumber,
                    IdentityCardNumber = accountCustomer.IdentityCardNumber,
                    Nationality = accountCustomer.Nationality,
                    PlaceOfOrigin = accountCustomer.PlaceOfOrigin,
                    PlaceOfResidence = accountCustomer.PlaceOfResidence,
                    DateOfExpiry = accountCustomer.DateOfExpiry,
                    Taxcode = accountCustomer.Taxcode,
                    BankName = accountCustomer.BankName,
                    BankNumber = accountCustomer.BankNumber,
                    Address = accountCustomer.Address,
                    DeviceToken = null,
                    Status = true,
                    AccountID = account.AccountID
                };

                var _customer = _mapper.Map<Customer>(customer);
                _customerServices.AddNewCustomer(_customer);

                return Ok(new
                {
                    message = "Tạo khách hàng thành công "
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Customer By Id")]
        public IActionResult UpdateCustomer([FromForm] CustomerUpdateDTO customer, Guid id)
        {
            try
            {
                var existingCustomer = _customerServices.GetCustomerByID(id);
                if (existingCustomer != null)
                {

                    if (!string.IsNullOrEmpty(customer.FullName))
                    {
                        existingCustomer.FullName = customer.FullName;
                    }
                    if (customer.DateOfBirth.HasValue)
                    {
                        existingCustomer.DateOfBirth = customer.DateOfBirth.Value;
                    }
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
                    if (!string.IsNullOrEmpty(customer.PlaceOfOrigin))
                    {
                        existingCustomer.PlaceOfOrigin = customer.PlaceOfOrigin;
                    }
                    if (!string.IsNullOrEmpty(customer.PlaceOfResidence))
                    {
                        existingCustomer.PlaceOfResidence = customer.PlaceOfResidence;
                    }
                    if (!string.IsNullOrEmpty(customer.DateOfExpiry))
                    {
                        existingCustomer.DateOfExpiry = customer.DateOfExpiry;
                    }
                    if (!string.IsNullOrEmpty(customer.Taxcode))
                    {
                        existingCustomer.Taxcode = customer.Taxcode;
                    }
                    if (!string.IsNullOrEmpty(customer.BankName))
                    {
                        existingCustomer.BankName = customer.BankName;
                    }
                    if (!string.IsNullOrEmpty(customer.BankNumber))
                    {
                        existingCustomer.BankNumber = customer.BankNumber;
                    }
                    if (!string.IsNullOrEmpty(customer.Address))
                    {
                        existingCustomer.Address = customer.Address;
                    }
                    if (!string.IsNullOrEmpty(customer.DeviceToken))
                    {
                        existingCustomer.DeviceToken = customer.DeviceToken;
                    }
                    if (customer.Status.HasValue)
                    {
                        existingCustomer.Status = customer.Status.Value;
                    }

                    _customerServices.UpdateCustomer(existingCustomer);

                    return Ok(new
                    {
                        message = "Cập nhật khách hàng thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Khách hàng không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("device-token")]
        [SwaggerOperation(Summary = "Update Customer Device Token using tokenJWT")]
        public IActionResult UpdateDeviceTokenCustomer([FromQuery] string tokenJWT, [FromQuery] string deviceToken)
        {
            var account = _jWTTokenService.ParseJwtToken(tokenJWT);

            if (account.AccountID.HasValue)
            {
                var existingAccount = _accountServices.GetAccountByID(account.AccountID.Value);

                if (existingAccount == null)
                {
                    return NotFound(new
                    {
                        message = "Tài khoản không tồn tại."
                    });
                }

                var existingCustomer = _customerServices.GetCustomerByAccountID(existingAccount.AccountID);
                if (existingCustomer == null)
                {
                    return NotFound(new
                    {
                        message = "Khách hàng không tồn tại."
                    });
                }

                existingCustomer.DeviceToken = deviceToken;
                _customerServices.UpdateCustomer(existingCustomer);

                return Ok(new
                {
                    message = "Update Device Token Successfully"
                });

            }
            else
            {
                return BadRequest(new
                {
                    message = "Invalid Account ID in token."
                });
            }



        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "DeleteCustomer")]
        public IActionResult DeleteCustomer(Guid id)
        {
            if (_customerServices.GetCustomerByID(id) == null)
            {
                return NotFound(new
                {
                    message = "Khách hàng không tồn tại."
                });
            }
            var customer = _customerServices.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Khách hàng không tồn tại."
                });
            }

            _customerServices.ChangeStatusCustomer(customer);


            return Ok(new
            {
                message = "Xóa khách hàng thành công"
            });
        }

    }
}
