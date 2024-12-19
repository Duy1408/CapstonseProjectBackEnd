using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstateProjectSaleBusinessObject.Admin;
using RealEstateProjectSaleBusinessObject.Model;
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
                        token = token,
                        role = "Admin"
                    });
                }

                var accountExists = _accountServices.CheckEmailOrPhone(account.EmailOrPhone);
                if (accountExists == null)
                {
                    return NotFound(new
                    {
                        message = "Email hoặc Số điện thoại không tồn tại"
                    });
                }


                var checkLogin = _accountServices.CheckLogin(account.EmailOrPhone!, account.Password!);
                if (checkLogin != null)
                {
                    var token = _jWTTokenService.CreateJWTToken(checkLogin);

                    return Ok(new
                    {
                        token = token,
                        role = accountExists.Role!.RoleName,
                        accountid = accountExists.AccountID,
                    });

                }
                return BadRequest(new
                {
                    message = "Password bạn đã nhập không chính xác."
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
                return BadRequest(new { message = "Không thể phân tích token JWT", error = ex.Message });
            }
        }

        [SwaggerOperation(Summary = "Check Email Existence", Description = "API này kiểm tra xem email có tồn tại trong hệ thống hay không.")]
        [SwaggerResponse(200, "Trả về true nếu email tồn tại, false nếu không tồn tại.")]
        [SwaggerResponse(500, "Nếu có lỗi từ phía máy chủ")]
        [HttpPost]
        [Route("check-email-exists")]
        public async Task<IActionResult> CheckEmailExists(CheckEmail email)
        {
            try
            {
                if (string.IsNullOrEmpty(email.Email))
                {
                    return BadRequest(new
                    {
                        message = "Email không được để trống."
                    });
                }

                var accountExists = _accountServices.CheckEmailOrPhone(email.Email);
                if (accountExists != null)
                {
                    return BadRequest(new
                    {
                        message = "Email đã được sử dụng"
                    });
                }

                return Ok(new
                {
                    message = "Email này có thể sử dụng."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Đã xảy ra lỗi khi kiểm tra email.",
                    error = ex.Message
                });
            }
        }

    }
}
