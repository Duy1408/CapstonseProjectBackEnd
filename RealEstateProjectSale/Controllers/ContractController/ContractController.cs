using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Stripe;
using Stripe.FinancialConnections;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Text;

namespace RealEstateProjectSale.Controllers.ContractController
{
    [Route("api/contracts")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractServices _contractServices;
        private readonly IPromotionDetailServices _promotiondetail;
        private readonly IPaymentProcessServices _paymentprocess;
        private readonly IBookingServices _bookServices;
        private readonly IFileUploadToBlobService _fileService;
        private readonly IMapper _mapper;
        private readonly ICustomerServices _customerServices;
        private readonly IPropertyServices _propertyServices;
        private readonly IDocumentTemplateService _documentTemplateService;
        private readonly IEmailService _emailService;
        private readonly IAccountServices _accountService;

        private readonly IProjectCategoryDetailServices _projectCategoryDetailServices;
        private readonly IProjectServices _projectServices;
        private readonly ISalespolicyServices _salespolicyServices;
        private readonly IUnitTypeServices _unitTypeServices;
        private readonly IPropertyTypeServices _propertyTypeServices;
        private readonly IPromotionServices _promotionServices;




        private readonly IContractPaymentDetailServices _contractDetailService;


        private static Dictionary<string, (string Otp, DateTime Expiration)> otpStorage = new Dictionary<string, (string, DateTime)>();

        public ContractController(IContractServices contractServices, IBookingServices bookServices,
                IFileUploadToBlobService fileService, IMapper mapper, IPromotionDetailServices promotiondetail, IPaymentProcessServices paymentprocess,
                ICustomerServices customerServices, IPropertyServices propertyServices,
                IDocumentTemplateService documentTemplateService, IEmailService emailService, IAccountServices accountService,

            IProjectCategoryDetailServices projectCategoryDetailServices, IProjectServices projectServices,
             ISalespolicyServices salespolicyServices, IUnitTypeServices unitTypeServices,
             IPropertyTypeServices propertyTypeServices, IPromotionServices promotionServices,

                IContractPaymentDetailServices contractDetailService

                )
        {
            _contractServices = contractServices;
            _bookServices = bookServices;
            _fileService = fileService;
            _mapper = mapper;
            _promotiondetail = promotiondetail;
            _paymentprocess = paymentprocess;
            _customerServices = customerServices;
            _propertyServices = propertyServices;
            _documentTemplateService = documentTemplateService;
            _emailService = emailService;
            _accountService = accountService;

            _projectCategoryDetailServices = projectCategoryDetailServices;
            _projectServices = projectServices;
            _salespolicyServices = salespolicyServices;
            _unitTypeServices = unitTypeServices;
            _propertyTypeServices = propertyTypeServices;
            _promotionServices = promotionServices;

            _contractDetailService = contractDetailService;

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Contract")]
        public IActionResult GetAllContract()
        {
            try
            {
                if (_contractServices.GetAllContract() == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại."
                    });
                }
                var contracts = _contractServices.GetAllContract();
                var response = _mapper.Map<List<ContractVM>>(contracts);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Contract By ID")]
        public IActionResult GetContractByID(Guid id)
        {
            var contract = _contractServices.GetContractByID(id);

            if (contract != null)
            {
                var responese = _mapper.Map<ContractVM>(contract);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Hợp đồng không tồn tại."
            });

        }

        [HttpGet("customer/{customerId}")]
        [SwaggerOperation(Summary = "Get Contract by customer ID")]
        public IActionResult GetContractByCustomerID(Guid customerId)
        {
            var contract = _contractServices.GetContractByCustomerID(customerId);

            if (contract != null)
            {
                var responese = contract.Select(contract => _mapper.Map<ContractVM>(contract)).ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Hợp đồng không tồn tại."
            });

        }

        [HttpGet("step-one")]
        [SwaggerOperation(Summary = "Show thông tin giao dịch ở bước 1 của Contract")]
        public IActionResult CustomerCheckInformation(Guid contractid)
        {

            var contract = _contractServices.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }
            var book = _bookServices.GetBookingById(contract.BookingID);
            if (book == null)
            {
                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });
            }
            var customer = _customerServices.GetCustomerByID(book.CustomerID);
            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Khách hàng không tồn tại."
                });
            }

            var propertyId = book.PropertyID.GetValueOrDefault(Guid.Empty);

            if (propertyId == Guid.Empty)
            {
                throw new ArgumentException("Booking không có căn hộ.");
            }

            var property = _propertyServices.GetPropertyById(propertyId);
            if (property == null)
            {
                return NotFound(new
                {
                    message = "Căn hộ không tồn tại."
                });
            }
            var customerresponese = _mapper.Map<CustomerVM>(customer);
            var propertyresponese = _mapper.Map<PropertyVM>(property);


            return Ok(new
            {
                customer = customerresponese,
                property = propertyresponese,
            });


        }

        [HttpPut("check-step-one")]
        [SwaggerOperation(Summary = "Khách hàng nhấn nút Xác nhận ở bước 1")]
        public IActionResult ShowCustomerDepositDocument(Guid contractid)
        {
            var contract = _contractServices.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }



            var htmlContent = _contractServices.GenerateDocumentDeposit(contract.ContractID);
            var pdfBytes = _documentTemplateService.GeneratePdfFromTemplate(htmlContent);
            string? blobUrl = null;
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            {
                blobUrl = _fileService.UploadSingleFile(pdfStream, contract.DocumentTemplate!.DocumentName, "contractdepositfile");
            }

            contract.ContractDepositFile = blobUrl;
            contract.Status = ContractStatus.ChoXacNhanTTDC.GetEnumDescription();
            _contractServices.UpdateContract(contract);
            return Ok(new
            {
                message = "Xác nhận thông tin giao dịch thành công."
            });

        }

        [HttpPost("step-two-send-otp")]
        [SwaggerOperation(Summary = "Gửi mã OTP qua mail cho khách hàng ở bước 2")]
        public async Task<IActionResult> sendEmail(Guid contractid)
        {

            try
            {

                var contract = _contractServices.GetContractByID(contractid);
                if (contract == null)
                {
                    return NotFound(new { message = "Hợp đồng không tồn tại." });
                }
                var customer = _customerServices.GetCustomerByID(contract.CustomerID);
                if (customer == null)
                {
                    return NotFound(new { message = "Khách hàng không tồn tại." });
                }
                var account = _accountService.GetAccountByID(customer.AccountID);

                if (account == null || string.IsNullOrEmpty(account.Email) || !IsValidEmail(account.Email))
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

        [HttpPost("step-two-verify-otp")]
        [SwaggerOperation(Summary = "Khách hàng xác nhận mã OTP ở bước 2")]
        public IActionResult verifyOtp(Guid contractid, string otp)
        {
            try
            {
                var contract = _contractServices.GetContractByID(contractid);
                if (contract == null)
                {
                    return NotFound(new { message = "Hợp đồng không tồn tại." });
                }
                var customer = _customerServices.GetCustomerByID(contract.CustomerID);
                if (customer == null)
                {
                    return NotFound(new { message = "Khách hàng không tồn tại." });
                }
                var account = _accountService.GetAccountByID(customer.AccountID);

                if (account == null || string.IsNullOrEmpty(account.Email) || !IsValidEmail(account.Email))
                {
                    return BadRequest(new { message = "Địa chỉ Email không hợp lệ." });
                }
                if (string.IsNullOrEmpty(account.Email) || string.IsNullOrEmpty(otp))
                {
                    return BadRequest(new { message = "Email and OTP là bắt buộc." });
                }
                if (otpStorage.TryGetValue(account.Email, out var otpEntry))
                {

                    if (otpEntry.Otp == otp && otpEntry.Expiration > DateTime.UtcNow)
                    {

                        otpStorage.Remove(account.Email);

                        contract.Status = ContractStatus.DaXacNhanTTDC.GetEnumDescription();
                        _contractServices.UpdateContract(contract);

                        var booking = _bookServices.GetBookingById(contract.BookingID);
                        booking.Status = BookingStatus.DaKyTTDC.GetEnumDescription();
                        _bookServices.UpdateBooking(booking);

                        var propertyId = booking.PropertyID.GetValueOrDefault(Guid.Empty);

                        if (propertyId == Guid.Empty)
                        {
                            throw new ArgumentException("Booking không có căn hộ.");
                        }
                        var property = _propertyServices.GetPropertyById(propertyId);
                        property.Status = PropertyStatus.DatCoc.GetEnumDescription();
                        _propertyServices.UpdateProperty(property);

                        //Gửi mail thông báo xán nhận TTDC thành công
                        Mailrequest mailrequest = new Mailrequest();
                        mailrequest.ToEmail = account.Email;
                        mailrequest.Subject = "Xác nhận thảo thuận đặt cọc";
                        mailrequest.Body =
                            $"<h5>THÔNG BÁO XÁC NHẬN THÀNH CÔNG THỎA THUẬN ĐẶT CỌC</h5>" +
                            $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>" +
                            $"<div>Thảo thuận đặt cọc của Quý khách đã được xác nhận. Quý khách có thể xem lại thông tin Thỏa thuận đặt cọc, đề nghị thanh toán. Quý khách vui lòng thực hiện chọn Phương án thanh toán, chính sách bán hàng</div>" +
                            $"<div>Hợp đồng mua bán:</div>" +
                            $"<div>Đường link xem Thỏa thuận đặt cọc</div>" +
                            $"<a href='{contract.ContractDepositFile}'>{contract.ContractDepositFile}</a>";

                        _emailService.SendEmailAsync(mailrequest);


                        return Ok(new { message = "Xác minh OTP thành công." });
                    }
                    else
                    {
                        return BadRequest(new { message = "OTP không hợp lệ hoặc đã hết hạn." });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Không tìm thấy OTP cho email này." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

        }



        [HttpGet("step-three")]
        [SwaggerOperation(Summary = "Show đợt thanh toán và gói khuyến mãi")]
        public IActionResult showPaymentProcessandPromotionDetail(Guid contractid)
        {

            var contract = _contractServices.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }
            var book = _bookServices.GetBookingById(contract.BookingID);
            if (book == null)
            {
                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });
            }
            var projectcategory = _projectCategoryDetailServices.GetProjectCategoryDetailByID(book.ProjectCategoryDetailID);
            if (projectcategory == null)
            {
                return NotFound(new
                {
                    message = "Loại căn hộ không tồn tại."
                });
            }
            //paymentprocess
            var project = _projectServices.GetProjectById(projectcategory.ProjectID);
            if (project == null)
            {
                return NotFound(new
                {
                    message = "Dự án không tồn tại."
                });
            }
            var salepolicy = _salespolicyServices.FindByProjectIdAndStatus(project.ProjectID);
            if (salepolicy == null)
            {
                return NotFound(new
                {
                    message = "Chính sách bán hàng không tồn tại."
                });
            }
            var paymentprocess = _paymentprocess.GetPaymentProcessBySalesPolicyID(salepolicy.SalesPolicyID);
            if (paymentprocess == null)
            {
                return NotFound(new
                {
                    message = "Thanh toán theo đợt không tồn tại."
                });
            }
            var paymentprocessresponese = _mapper.Map<List<PaymentProcessVM>>(paymentprocess);
            //promtotiondetail
            var propertyId = book.PropertyID.GetValueOrDefault(Guid.Empty);

            if (propertyId == Guid.Empty)
            {
                throw new ArgumentException("Booking không có căn hộ.");
            }

            var property = _propertyServices.GetPropertyById(propertyId);
            if (property == null)
            {
                return NotFound(new
                {
                    message = "Căn hộ không tồn tại."
                });
            }
            var unittypeId = property.UnitTypeID.GetValueOrDefault(Guid.Empty);
            if (unittypeId == Guid.Empty)
            {
                throw new ArgumentException("Property không có unittype");
            }

            var unittype = _unitTypeServices.GetUnitTypeByID(unittypeId);
            if (unittype == null)
            {
                return NotFound(new
                {
                    message = "Unittype không tồn tại."
                });
            }
            var propertytypeId = unittype.PropertyTypeID.GetValueOrDefault(Guid.Empty);
            if (propertytypeId == Guid.Empty)
            {
                throw new ArgumentException("unittype không có propertytype");
            }
            var propertytype = _propertyTypeServices.GetPropertyTypeByID(propertytypeId);
            if (propertytype == null)
            {
                return NotFound(new
                {
                    message = "propertytype không tồn tại."
                });
            }
            var promotion = _promotionServices.FindBySalesPolicyIdAndStatus(salepolicy.SalesPolicyID);
            if (promotion == null)
            {
                return NotFound(new
                {
                    message = "Khuyến mãi không tồn tại."
                });
            }
            var promotiondetail = _promotiondetail.GetDetailByPromotionIDPropertyTypeID(promotion.PromotionID, propertytypeId);
            var promotiondetailresponese = _mapper.Map<PromotionDetailVM>(promotiondetail);
            return Ok(new
            {
                promotiondetail = promotiondetailresponese,
                paymentprocess = paymentprocessresponese,
            });
        }

        [HttpPut("check-step-three")]
        [SwaggerOperation(Summary = "Khách hàng nhấn nút Xác nhận ở bước 3")]
        public IActionResult CustomerChoosePromotion(Guid contractid, Guid promotiondetailid, Guid paymentprocessid)
        {
            try
            {
                var contract = _contractServices.GetContractByID(contractid);
                if (contract == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại."
                    });
                }

                var promotiondetail = _promotiondetail.GetPromotionDetailByID(promotiondetailid);
                if (promotiondetail == null)
                {
                    return NotFound(new
                    {
                        message = "Gói khuyến mãi không tồn tại."
                    });
                }
                var paymentprocess = _paymentprocess.GetPaymentProcessById(paymentprocessid);
                if (paymentprocess == null)
                {
                    return NotFound(new
                    {
                        message = "Phương thức thanh toán không tồn tại."
                    });
                }

                var documentReservation = _documentTemplateService.GetDocumentByDocumentName("Phiếu tính giá");
                if (documentReservation == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại"
                    });
                }

                contract.PaymentProcessID = paymentprocessid;
                contract.PromotionDetailID = promotiondetailid;
                contract.DocumentTemplateID = documentReservation.DocumentTemplateID;
                contract.UpdatedTime = DateTime.Now;
                _contractServices.UpdateContract(contract);

                var htmlContent = _contractServices.GenerateDocumentPriceSheet(contract.ContractID);
                var pdfBytes = _documentTemplateService.GeneratePdfFromTemplate(htmlContent);
                string? blobUrl = null;
                using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
                {
                    blobUrl = _fileService.UploadSingleFile(pdfStream, contract.DocumentTemplate!.DocumentName, "contractdepositfile");
                }

                contract.PriceSheetFile = blobUrl;
                contract.Status = ContractStatus.DaXacNhanCSBH.GetEnumDescription();
                _contractServices.UpdateContract(contract);

                var customer = _customerServices.GetCustomerByID(contract.CustomerID);
                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Khách hàng không tồn tại."
                    });
                }

                var account = _accountService.GetAccountByID(customer.AccountID);
                if (account == null || string.IsNullOrEmpty(account.Email) || !IsValidEmail(account.Email))
                {
                    return BadRequest(new { message = "Địa chỉ Email không hợp lệ." });
                }

                //Gửi mail thông báo yêu cầu xác nhận PTG
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = account.Email;
                mailrequest.Subject = "Yêu Cầu Xác Nhận Phiếu Tính Giá";
                mailrequest.Body =
                    $"<h5>THÔNG BÁO YÊU CẦU XÁC NHẬN PHIẾU TÍNH GIÁ</h5>" +
                    $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>" +
                    $"<div>Quý khách vui lòng kiểm tra và xác nhận thông tin Phiếu tính giá.</div>" +
                    $"<div>Đường link xem Phiếu tính giá</div>" +
                    $"<a href='{contract.PriceSheetFile}'>{contract.PriceSheetFile}</a>";

                _emailService.SendEmailAsync(mailrequest);


                return Ok(new
                {
                    message = "Chọn phương thức thanh toán và chính sách ưu đãi thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("step-four")]
        [SwaggerOperation(Summary = "Show thông tin khách hàng ở bước 4 của Contract")]
        public IActionResult ShowInformationCustomer(Guid contractid)
        {

            var contract = _contractServices.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }

            var customer = _customerServices.GetCustomerByID(contract.CustomerID);
            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Khách hàng không tồn tại."
                });
            }
            var responese = _mapper.Map<CustomerVM>(customer);

            return Ok(responese);

        }

        [HttpPut("check-step-four")]
        [SwaggerOperation(Summary = "Khách hàng nhấn nút Xác nhận Phiếu tính giá ở bước 4")]
        public IActionResult CustomerConfirmPriceList(Guid contractid)
        {
            var contract = _contractServices.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }
            var customer = _customerServices.GetCustomerByID(contract.CustomerID);
            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Khách hàng không tồn tại."
                });
            }

            var account = _accountService.GetAccountByID(customer.AccountID);
            if (account == null || string.IsNullOrEmpty(account.Email) || !IsValidEmail(account.Email))
            {
                return BadRequest(new { message = "Địa chỉ Email không hợp lệ." });
            }

            contract.Status = ContractStatus.DaXacNhanPhieuTinhGia.GetEnumDescription();
            _contractServices.UpdateContract(contract);

            //Gửi mail thư mời thanh toán tiền
            Mailrequest mailrequest = new Mailrequest();
            mailrequest.ToEmail = account.Email;
            mailrequest.Subject = "Thư mời thanh toán tiền";
            mailrequest.Body =
                $"<h5>THƯ MỜI THANH TOÁN TIỀN</h5>" +
                $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>" +
                $"<div>Quý khách đã xác nhận Phiếu tính giá thành công trên Thủ tục JustHome. Quý khách vui lòng thanh toán tiền Đợt 1 Hợp đồng mua bán theo thông tin trên Phiếu tính giá và upload ảnh chụp Ủy nhiệm chi.</div>" +
                $"<div>Đường link xem Phiếu tính giá</div>" +
                $"<a href='{contract.PriceSheetFile}'>{contract.PriceSheetFile}</a>";

            _emailService.SendEmailAsync(mailrequest);


            _contractServices.CreateContractPaymentDetail(contractid);

            return Ok(new
            {
                message = "Xác nhận Phiếu tính giá thành công."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Contract")]
        public IActionResult AddNewContract([FromForm] ContractRequestDTO contract)
        {
            try
            {
                string? blobUrl = null;
                var contractFile = contract.ContractDepositFile;
                if (contractFile != null)
                {
                    using (var pdfStream = contractFile.OpenReadStream())
                    {
                        blobUrl = _fileService.UploadSingleFile(pdfStream, contractFile.FileName, "contractdepositfile");
                    }
                }

                string nextContractCode = GenerateNextContractCode();

                var newContract = new ContractCreateDTO
                {
                    ContractID = Guid.NewGuid(),
                    ContractCode = nextContractCode,
                    ContractType = contract.ContractType,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,

                    ExpiredTime = contract.ExpiredTime,
                    TotalPrice = contract.TotalPrice,
                    Description = contract.Description,
                    ContractDepositFile = contract.ContractDepositFile,
                    ContractSaleFile = null,
                    PriceSheetFile = null,
                    Status = ContractStatus.ChoXacNhanTTGD.GetEnumDescription(),
                    DocumentTemplateID = contract.DocumentTemplateID,
                    BookingID = contract.BookingID,
                    CustomerID = contract.CustomerID,
                    PaymentProcessID = contract.PaymentProcessID,
                    PromotionDetailID = contract.PromotionDetailID

                };

                var _contract = _mapper.Map<RealEstateProjectSaleBusinessObject.BusinessObject.Contract>(newContract);
                _contract.ContractDepositFile = blobUrl;
                _contractServices.AddNewContract(_contract);

                return Ok(new
                {
                    message = "Tạo hợp đồng thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Contract by ID")]
        public IActionResult UpdateContract([FromForm] ContractUpdateDTO contract, Guid id)
        {
            try
            {
                string? blobUrl1 = null, blobUrl2 = null, blobUrl3 = null;
                var depositFile = contract.ContractDepositFile;
                var saleFile = contract.ContractSaleFile;
                var priceFile = contract.PriceSheetFile;
                if (depositFile != null)
                {
                    using (var pdfDepositStream = depositFile.OpenReadStream())
                    {
                        blobUrl1 = _fileService.UploadSingleFile(pdfDepositStream, depositFile.FileName, "contractdepositfile");
                    }
                }
                if (saleFile != null)
                {
                    using (var pdfSaleStream = saleFile.OpenReadStream())
                    {
                        blobUrl2 = _fileService.UploadSingleFile(pdfSaleStream, saleFile.FileName, "contractsalefile");
                    }
                }
                if (priceFile != null)
                {
                    using (var pdfSaleStream = priceFile.OpenReadStream())
                    {
                        blobUrl3 = _fileService.UploadSingleFile(pdfSaleStream, priceFile.FileName, "pricefile");
                    }
                }

                var existingContract = _contractServices.GetContractByID(id);
                if (existingContract != null)
                {
                    if (!string.IsNullOrEmpty(contract.ContractCode))
                    {
                        existingContract.ContractCode = contract.ContractCode;
                    }

                    if (!string.IsNullOrEmpty(contract.ContractType))
                    {
                        existingContract.ContractType = contract.ContractType;
                    }
                    if (contract.CreatedTime.HasValue)
                    {
                        existingContract.CreatedTime = contract.CreatedTime.Value;
                    }
                    if (contract.UpdatedTime.HasValue)
                    {
                        existingContract.UpdatedTime = contract.UpdatedTime.Value;
                    }

                    if (contract.ExpiredTime.HasValue)
                    {
                        existingContract.ExpiredTime = contract.ExpiredTime.Value;
                    }
                    if (contract.TotalPrice.HasValue)
                    {
                        existingContract.TotalPrice = contract.TotalPrice.Value;
                    }
                    if (!string.IsNullOrEmpty(contract.Description))
                    {
                        existingContract.Description = contract.Description;
                    }
                    if (!string.IsNullOrEmpty(contract.Status))
                    {
                        existingContract.Status = contract.Status;
                    }
                    if (contract.DocumentTemplateID.HasValue)
                    {
                        existingContract.DocumentTemplateID = contract.DocumentTemplateID.Value;
                    }
                    if (contract.BookingID.HasValue)
                    {
                        existingContract.BookingID = contract.BookingID.Value;
                    }
                    if (contract.CustomerID.HasValue)
                    {
                        existingContract.CustomerID = contract.CustomerID.Value;
                    }
                    if (contract.PaymentProcessID.HasValue)
                    {
                        existingContract.PaymentProcessID = contract.PaymentProcessID.Value;
                    }
                    if (contract.PromotionDetailID.HasValue)
                    {
                        existingContract.PromotionDetailID = contract.PromotionDetailID.Value;
                    }
                    if (blobUrl1 != null)
                    {
                        existingContract.ContractDepositFile = blobUrl1;
                    }
                    if (blobUrl2 != null)
                    {
                        existingContract.ContractSaleFile = blobUrl2;
                    }
                    if (blobUrl3 != null)
                    {
                        existingContract.PriceSheetFile = blobUrl3;
                    }

                    existingContract.UpdatedTime = DateTime.Now;
                    _contractServices.UpdateContract(existingContract);

                    return Ok(new
                    {
                        message = "Cập nhật hợp đồng thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Contract by ID")]
        public IActionResult DeleteContract(Guid id)
        {

            var contract = _contractServices.GetContractByID(id);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }

            _contractServices.ChangeStatusContract(contract);


            return Ok(new
            {
                message = "Xóa hợp đồng thành công"
            });
        }

        private string GenerateNextContractCode()
        {
            // Lấy số hợp đồng hiện tại (có thể từ DB hoặc cache)
            var lastContract = _contractServices.GetLastContract();

            int nextNumber = 1;

            // Nếu có hợp đồng trước đó, lấy số lớn nhất và tăng lên
            if (lastContract != null)
            {
                string lastCode = lastContract.ContractCode.Split('/')[0];  // Lấy phần số trước dấu "/"
                int.TryParse(lastCode, out nextNumber);
                nextNumber++;  // Tăng số lên
            }

            // Định dạng mã hợp đồng, số có 4 chữ số kèm phần định danh "/TTĐC"
            string nextContractCode = nextNumber.ToString() + "/TTĐC";

            return nextContractCode;
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
