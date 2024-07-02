using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                var _account = _mapper.Map<Account>(account);
                _accountServices.AddNewAccount(_account);

                return Ok("Create Account Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(AccountUpdateDTO account, Guid id)
        {
            try
            {
                var existingAccount = _accountServices.GetAccountByID(id);
                if (existingAccount != null)
                {
                    account.AccountID = existingAccount.AccountID;
                    account.RoleID = existingAccount.RoleID;

                    var _account = _mapper.Map<Account>(account);
                    _accountServices.UpdateAccount(_account);

                    return Ok("Update Successfully");

                }

                return NotFound("Staff not found.");

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
