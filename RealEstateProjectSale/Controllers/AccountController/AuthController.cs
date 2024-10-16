using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstateProjectSaleBusinessObject.Admin;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.AccountController
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly IJWTTokenService _jWTTokenService;
        private readonly AdminAccountConfig _adminConfig;

        public AuthController(IAccountServices accountServices, IJWTTokenService jWTTokenService, IOptions<AdminAccountConfig> adminConfig)
        {
            _accountServices = accountServices;
            _jWTTokenService = jWTTokenService;
            _adminConfig = adminConfig.Value;
        }

        [SwaggerOperation(Summary = "Login Account", Description = "API này request body là Email hoặc Phone và password.")]
        [SwaggerResponse(200, "Trả về JWT token")]
        [SwaggerResponse(500, "Nếu có lỗi từ phía máy chủ")]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginVM account)
        {
            try
            {
                if (account.EmailOrPhone == _adminConfig.Email && account.Password == _adminConfig.Password)
                {
                    var token = _jWTTokenService.CreateAdminJWTToken();

                    return Ok(new
                    {
                        token = token
                    });
                }

                var accountExists = _accountServices.CheckEmailOrPhone(account.EmailOrPhone);
                if (accountExists == null)
                {
                    return NotFound(new
                    {
                        message = "Email or PhoneNumber doesn't exist"
                    });
                }


                var checkLogin = _accountServices.CheckLogin(account.EmailOrPhone!, account.Password!);
                if (checkLogin != null)
                {
                    var token = _jWTTokenService.CreateJWTToken(checkLogin);

                    return Ok(new
                    {
                        token = token
                    });

                }
                return BadRequest(new
                {
                    message = "The password you entered is incorrect"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Phân tích JWT token", Description = "API này request body là 1 chuỗi token lúc login nhận được.")]
        [SwaggerResponse(200, "Trả về thông tin account đã login")]
        [SwaggerResponse(500, "Nếu có lỗi từ phía máy chủ")]
        [HttpGet]
        [Route("token/parse")]
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
