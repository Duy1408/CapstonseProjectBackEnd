using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get All Account")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách tài khoản.", typeof(List<AccountVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Tài khoản không tồn tại.")]
        public IActionResult GetAllAccount()
        {
            try
            {
                if (_accountServices.GetAllAccount() == null)
                {
                    return NotFound(new
                    {
                        message = "Không có tài khoản nào tồn tại."
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

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Account By ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về thông tin tài khoản.", typeof(AccountVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Tài khoản không tồn tại.")]
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
                message = "Tài khoản không tồn tại."
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Account")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tạo tài khoản thành công.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Tài khoản đã tồn tại.")]
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
                        message = "Tài khoản đã tồn tại."
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
                    message = "Tạo tài khoản thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Account")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật tài khoản thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Tài khoản không tồn tại.")]
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
                        message = "Cập nhật tài khoản thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Tài khoản không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Account")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xóa tài khoản thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Tài khoản không tồn tại.")]
        public IActionResult DeleteAccount(Guid id)
        {

            var account = _accountServices.GetAccountByID(id);
            if (account == null)
            {
                return NotFound(new
                {
                    message = "Tài khoản không tồn tại."
                });
            }

            _accountServices.ChangeStatusAccount(account);

            return Ok(new
            {
                message = "Xóa tài khoản thành công."
            });
        }

    }
}
