using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleServices.IServices;
using System.Text;

namespace RealEstateProjectSale.Controllers.EmailController
{
    [Route("api/emails")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IContractServices _contract;
        private readonly ICustomerServices _customer;
        private readonly IAccountServices _account;


        private readonly IEmailService _emailService;
        private static Dictionary<string, (string Otp, DateTime Expiration)> otpStorage = new Dictionary<string, (string, DateTime)>();
        public EmailController(IEmailService emailService, IContractServices contract, ICustomerServices customer, IAccountServices account)
        {
            _emailService = emailService;
            _customer = customer;
            _contract = contract;
            _account = account;
        }

        [HttpPost("step-two")]
        public async Task<IActionResult> sendDeposittoemail(string email, Guid contractid)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                {
                    return BadRequest(new { message = "Lỗi email" });
                }
                var contract = _contract.GetContractByID(contractid);
                if(contract==null)
                {
                    return BadRequest(new { message = "Hợp đồng không tồn tại" });
                }

                contract.Status = ContractStatus.DaXacNhanTTDC.GetEnumDescription();
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = email;
                mailrequest.Subject = "Xác nhận thảo thuận đặt cọc";
                mailrequest.Body =
                    $"<h5>THÔNG BÁO XÁC NHẬN THÀNH CÔNG THỎA THUẬN ĐẶT CỌC</h5>" +
                    $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>"+
                    $"<div>Thảo thuận đặt cọc của Quý khách đã được xác nhận. Quý khách có thể xem lại thông tin Thỏa thuận đặt cọc, đề nghị thanh toán. Quý khách vui lòng thực hiện chọn Phương án thanh toán, chính sách bán hàng</div>"+
                    $"<div>Hợp đồng mua bán:</div>" +
                    $"<div>.Đường link xem Thỏa thuận đặt cọc</div>" +
                    $"<a href='{contract.ContractDepositFile}'>{contract.ContractDepositFile}</a>";
            
                await _emailService.SendEmailAsync(mailrequest);
                return Ok(new { message = "Xác nhận thỏa thuận đặt cọc đã gửi về mail " });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }





        [HttpPost("sendEmail")]
        public async Task<IActionResult> sendEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                {
                    return BadRequest(new { message = "Lỗi email" });
                }
                string otp = GenerateOTP();
                DateTime expirationTime = DateTime.UtcNow.AddMinutes(1);
                otpStorage[email] = (otp, expirationTime);
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = email;
                mailrequest.Subject = "OTP Verification Code";
                mailrequest.Body = $"Hello, your OTP code is: {otp}";
                await _emailService.SendEmailAsync(mailrequest);
                return Ok(new { message = "OTP đã được gửi về email " });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }


        [HttpPost("verify-otp")]
        public IActionResult verifyOtp(string email, string otp)
        {



            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp))
            {
                return BadRequest(new { message = "Yêu cầu nhập email và otp" });
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


        [HttpPost("sendotpbycontract")]
        public async Task<IActionResult> sendEmail(Guid contractid)
        {

            try { 

                var contract = _contract.GetContractByID(contractid);
            if (contract == null)
            {
                    return NotFound(new { message = "Hợp đồng không tồn tại." });
                }
                var customer = _customer.GetCustomerByID(contract.CustomerID);
                if (customer == null)
                {
                    return NotFound(new { message = "Khách hàng không tồn tại." });
                }
                var account = _account.GetAccountByID(customer.AccountID);

                if(account == null || string.IsNullOrEmpty(account.Email)|| !IsValidEmail(account.Email))
                {
                    return BadRequest(new { message = "Lỗi email." });
                }
            
                
                string otp = GenerateOTP();
                DateTime expirationTime = DateTime.UtcNow.AddMinutes(3);
                otpStorage[account.Email] = (otp, expirationTime);
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = account.Email;
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






        [HttpPost("verify-otp-by-contract")]
        public IActionResult verifyOtp(Guid contractid, string otp)
        {
           
            var contract = _contract.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new { message = "Hợp đồng không tồn tại." });
            }
            var customer = _customer.GetCustomerByID(contract.CustomerID);
            if (customer == null)
            {
                return NotFound(new { message = "Khách hàng không tồn tại." });
            }
            var account = _account.GetAccountByID(customer.AccountID);

            if (account == null || string.IsNullOrEmpty(account.Email) || !IsValidEmail(account.Email))
            {
                return BadRequest(new { message = "Invalid email address." });
            }
            if (string.IsNullOrEmpty(account.Email) || string.IsNullOrEmpty(otp))
            {
                return BadRequest(new { message = "Email and OTP are required." });
            }
            if (otpStorage.TryGetValue(account.Email, out var otpEntry))
            {

                if (otpEntry.Otp == otp && otpEntry.Expiration > DateTime.UtcNow)
                {

                    otpStorage.Remove(account.Email);
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
