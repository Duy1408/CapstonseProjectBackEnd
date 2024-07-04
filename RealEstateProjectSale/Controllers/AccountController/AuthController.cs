using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.AccountController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly IJWTTokenService _jWTTokenService;

        public AuthController(IAccountServices accountServices, IJWTTokenService jWTTokenService)
        {
            _accountServices = accountServices;
            _jWTTokenService = jWTTokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM account)
        {
            try
            {
                var checkLogin = _accountServices.CheckLogin(account.Email!, account.Password!);
                if (checkLogin != null)
                {
                    var token = _jWTTokenService.CreateJWTToken(checkLogin);

                    var accountVM = new AccountVM
                    {
                        AccountID = checkLogin.AccountID,
                        Email = checkLogin.Email,
                        Password = checkLogin.Password,
                        Status = checkLogin.Status,
                        RoleName = checkLogin.Role.RoleName
                    };

                    return Ok(new
                    {
                        message = " Login Successfully",
                        data = accountVM,
                        token = token
                    });
                }
                return NotFound(new
                {
                    message = " Login Fail",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
