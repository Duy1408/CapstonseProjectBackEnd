using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleServices.Helper;
using RealEstateProjectSaleServices.IServices;
using System.Text;

namespace RealEstateProjectSale.Controllers.EmailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private static Dictionary<string, (string Otp, DateTime Expiration)> otpStorage = new Dictionary<string, (string, DateTime)>();
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [HttpPost("SendMail")]
        public async Task<IActionResult> SendEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                {
                    return BadRequest(new { message = "Invalid email address." });
                }               
                string otp = GenerateOTP();
                DateTime expirationTime = DateTime.UtcNow.AddMinutes(3);                 
                otpStorage[email] = (otp, expirationTime);
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = email;
                mailrequest.Subject = "OTP Verification Code";
                mailrequest.Body = $"Hello, your OTP code is: {otp}";    
                await _emailService.SendEmailAsync(mailrequest);
                return Ok(new { message = "OTP has been sent to your email. " });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp(string email, string otp)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp))
            {
                return BadRequest(new { message = "Email and OTP are required." });
            }          
            if (otpStorage.TryGetValue(email, out var otpEntry))
            {
               
                if (otpEntry.Otp == otp && otpEntry.Expiration > DateTime.UtcNow)
                {
                    
                    otpStorage.Remove(email);
                    return Ok(new { message = "OTP verification successful." });
                }
                else
                {
                    return BadRequest(new { message = "Invalid or expired OTP." });
                }
            }
            else
            {
                return BadRequest(new { message = "OTP not found for this email." });
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateOTP(int length = 6)
        {
            var random = new Random();
            var otp = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                otp.Append(random.Next(0, 10)); 
            }

            return otp.ToString();
        }
    }
}
