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

namespace RealEstateProjectSale.Controllers.AccountController
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountServices _accountServices;
        private readonly IMapper _mapper;

        public AccountController(IAccountServices accountServices, IMapper mapper)
        {
            _accountServices = accountServices;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Account")]
        public IActionResult GetAllAccount()
        {
            try
            {
                if (_accountServices.GetAllAccount() == null)
                {
                    return NotFound(new
                    {
                        message = "Account không tồn tại."
                    });
                }
                var accounts = _accountServices.GetAllAccount();
                var response = _mapper.Map<List<AccountVM>>(accounts);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Account By ID")]
        public IActionResult GetAccountByID(Guid id)
        {
            var account = _accountServices.GetAccountByID(id);

            if (account != null)
            {
                var responese = _mapper.Map<AccountVM>(account);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Account không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Account")]
        public IActionResult AddNewAccount(AccountCreateDTO account)
        {
            try
            {
                var checkEmail = _accountServices.GetAllAccount().Where(u =>
                u.Email.Equals(account.Email)).FirstOrDefault();
                if (checkEmail != null)
                {
                    return BadRequest(new
                    {
                        message = "Email đã tồn tại"
                    });
                }
                var newAccount = new AccountCreateDTO
                {
                    AccountID = Guid.NewGuid(),
                    Email = account.Email,
                    Password = account.Password,
                    Status = true,
                    RoleID = account.RoleID
                };

                var _account = _mapper.Map<Account>(newAccount);
                _accountServices.AddNewAccount(_account);

                return Ok(new
                {
                    message = "Tạo Account thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Account")]
        public IActionResult UpdateAccount([FromForm] AccountUpdateDTO account, Guid id)
        {
            try
            {
                var existingAccount = _accountServices.GetAccountByID(id);
                if (existingAccount != null)
                {
                    if (!string.IsNullOrEmpty(account.Email))
                    {
                        existingAccount.Email = account.Email;
                    }
                    if (!string.IsNullOrEmpty(account.Password))
                    {
                        existingAccount.Password = account.Password;
                    }
                    if (account.Status.HasValue)
                    {
                        existingAccount.Status = account.Status.Value;
                    }
                    if (account.RoleID.HasValue)
                    {
                        existingAccount.RoleID = account.RoleID.Value;
                    }

                    _accountServices.UpdateAccount(existingAccount);

                    return Ok(new
                    {
                        message = "Cập nhật Account thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Account không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Account")]
        public IActionResult DeleteAccount(Guid id)
        {

            var account = _accountServices.GetAccountByID(id);
            if (account == null)
            {
                return NotFound(new
                {
                    message = "Account không tồn tại."
                });
            }

            _accountServices.ChangeStatusAccount(account);

            return Ok(new
            {
                message = "Xóa Account thành công"
            });
        }

    }
}
