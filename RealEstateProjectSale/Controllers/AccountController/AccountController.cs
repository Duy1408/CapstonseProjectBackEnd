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

namespace RealEstateProjectSale.Controllers.AccountController
{
    [Route("api/[controller]")]
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
        public IActionResult GetAllAccount()
        {
            try
            {
                if (_accountServices.GetAllAccount() == null)
                {
                    return NotFound();
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
        public IActionResult GetAccountByID(Guid id)
        {
            var account = _accountServices.GetAccountByID(id);

            if (account != null)
            {
                var responese = _mapper.Map<AccountVM>(account);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult AddNewAccount(AccountCreateDTO account)
        {
            try
            {
                var checkEmail = _accountServices.GetAllAccount().Where(u =>
                u.Email.Equals(account.Email)).FirstOrDefault();
                if (checkEmail != null)
                {
                    return BadRequest("Email Existed");
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

                return Ok("Create Account Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount([FromForm] AccountUpdateDTO account, Guid id)
        {
            try
            {
                var existingAccount = _accountServices.GetAccountByID(id);
                if (existingAccount != null)
                {
                    account.AccountID = existingAccount.AccountID;

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
                    if (account.RoleID != null)
                    {
                        existingAccount.RoleID = (Guid)account.RoleID;
                    }

                    _accountServices.UpdateAccount(existingAccount);

                    return Ok("Update Account Successfully");

                }

                return NotFound("Account not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(Guid id)
        {

            var account = _accountServices.GetAccountByID(id);
            if (account == null)
            {
                return NotFound();
            }

            _accountServices.ChangeStatusAccount(account);


            return Ok("Delete Successfully");
        }

    }
}
