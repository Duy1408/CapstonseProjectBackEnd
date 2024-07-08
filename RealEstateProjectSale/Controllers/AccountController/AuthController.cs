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

                    return Ok(token);

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

        [HttpGet]
        public async Task<IActionResult> ParseJwtToken([FromQuery] string token)
        {
            try
            {

                var account = _jWTTokenService.ParseJwtToken(token);
                return Ok(account);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to parse JWT token", error = ex.Message });
            }
        }

    }
}
