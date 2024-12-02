using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using Google.Api.Gax.ResourceNames;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSale.Helpers;
using RealEstateProjectSale.SwaggerResponses;
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
using System.Security.Principal;
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
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách hợp đồng.", typeof(List<ContractVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public IActionResult GetAllContract()
        {
            try
            {
                if (_contractServices.GetAllContract() == null)
                {
                    return NotFound(new
                    {
                        message = " Không có hợp đồng nào tồn tại."
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
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về thông tin hợp đồng.", typeof(ContractVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
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
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách hợp đồng của khách hàng.", typeof(List<ContractResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public IActionResult GetContractByCustomerID(Guid customerId)
        {
            var contracts = _contractServices.GetContractByCustomerID(customerId);

            if (contracts != null)
            {
                var responese = contracts.Select(contract => new ContractResponse
                {
                    ContractID = contract.ContractID,
                    ProjectName = contract.Booking!.ProjectCategoryDetail!.Project!.ProjectName,
                    PropertyCode = contract!.Booking!.Property!.PropertyCode,
                    PriceSold = contract.Booking.Property.PriceSold,
                    ExpiredTime = contract.ExpiredTime,
                    Status = contract.Status
                }).OrderBy(contract => contract.ExpiredTime)
                  .ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Hợp đồng không tồn tại."
            });

        }

        [HttpGet("step-one")]
        [SwaggerOperation(Summary = "Show thông tin giao dịch ở bước 1 của Contract")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về thông tin khách hàng, căn hộ và dự án.", typeof(ContractStepOneResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy dữ liệu liên quan đến hợp đồng.")]
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

            var projectdetailId = property.ProjectCategoryDetailID.GetValueOrDefault(Guid.Empty);

            if (projectdetailId == Guid.Empty)
            {
                throw new ArgumentException("Căn hộ không tồn tại.");
            }
            var projectdetail = _projectCategoryDetailServices.GetProjectCategoryDetailByID(projectdetailId);
            if (projectdetail == null)
            {
                return NotFound(new
                {
                    message = "Loại căn hộ không tồn tại."
                });
            }

            var project = _projectServices.GetProjectById(projectdetail.ProjectID);
            if (project == null)
            {
                return NotFound(new
                {
                    message = "Dự án không tồn tại."
                });
            }

            var customerresponese = _mapper.Map<CustomerVM>(customer);
            var propertyresponese = _mapper.Map<PropertyVM>(property);
            var projectresponese = new ProjectVM
            {
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                Location = project.Location,
                Investor = project.Investor,
                GeneralContractor = project.GeneralContractor,
                DesignUnit = project.DesignUnit,
                TotalArea = project.TotalArea,
                Scale = project.Scale,
                BuildingDensity = project.BuildingDensity,
                TotalNumberOfApartment = project.TotalNumberOfApartment,
                LegalStatus = project.LegalStatus,
                HandOver = project.HandOver,
                Convenience = project.Convenience,
                Images = new List<string> { project.Image?.Split(',').FirstOrDefault() ?? string.Empty },
                Status = project.Status,
                PaymentPolicyID = project.PaymentPolicyID,
                PaymentPolicyName = project.PaymentPolicy?.PaymentPolicyName
            };


            return Ok(new
            {
                customer = customerresponese,
                property = propertyresponese,
                project = projectresponese,
            });


        }

        [HttpPut("check-step-one")]
        [SwaggerOperation(Summary = "Khách hàng nhấn nút Xác nhận ở bước 1")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xác nhận thông tin giao dịch thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
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
            contract.UpdatedTime = DateTime.Now;
            _contractServices.UpdateContract(contract);
            return Ok(new
            {
                message = "Xác nhận thông tin giao dịch thành công."
            });

        }

        [HttpPost("step-two-send-otp")]
        [SwaggerOperation(Summary = "Gửi mã OTP qua mail cho khách hàng ở bước 2")]
        [SwaggerResponse(StatusCodes.Status200OK, "Mã OTP đã được gửi thành công qua email.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public async Task<IActionResult> SendEmailDeposit(Guid contractid)
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
                return Ok(new { message = "Mã OTP đã được gửi thành công qua email." });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("step-two-verify-otp")]
        [SwaggerOperation(Summary = "Khách hàng xác nhận mã OTP ở bước 2")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xác minh OTP thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public IActionResult VerifyOtpDeposit(Guid contractid, string otp)
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
                        mailrequest.Subject = "Xác nhận Thỏa thuận đặt cọc";
                        mailrequest.Body =
                            $"<h5>THÔNG BÁO XÁC NHẬN THÀNH CÔNG THỎA THUẬN ĐẶT CỌC</h5>" +
                            $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>" +
                            $"<div>Thảo thuận đặt cọc của Quý khách đã được xác nhận. Quý khách có thể xem lại thông tin Thỏa thuận đặt cọc, đề nghị thanh toán. Quý khách vui lòng thực hiện chọn Phương án thanh toán, chính sách bán hàng</div>" +
                            $"<div>Đường link xem Thỏa thuận đặt cọc</div>" +
                            $"<a href='{contract.ContractDepositFile}'>{contract.ContractDepositFile}</a>";

                        _emailService.SendEmailAsync(mailrequest);

                        contract.Status = ContractStatus.DaXacNhanTTDC.GetEnumDescription();
                        contract.UpdatedTime = DateTime.Now;
                        contract.ExpiredTime = DateTime.Now.AddDays(1);
                        _contractServices.UpdateContract(contract);

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
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách PaymentProcess và PromotionDetail", typeof(PaymentPromotionResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nếu không tìm thấy dữ liệu hoặc có lỗi liên quan")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Nếu có lỗi đầu vào hoặc logic")]
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
                promotionDetail = promotiondetailresponese,
                paymentProcess = paymentprocessresponese,
            });
        }

        [HttpPut("check-step-three")]
        [SwaggerOperation(Summary = "Khách hàng nhấn nút Xác nhận ở bước 3")]
        [SwaggerResponse(StatusCodes.Status200OK, "Chọn phương thức thanh toán và chính sách ưu đãi thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng, gói khuyến mãi hoặc phương thức thanh toán.")]
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
                    blobUrl = _fileService.UploadSingleFile(pdfStream, contract.DocumentTemplate!.DocumentName, "pricefile");
                }

                contract.PriceSheetFile = blobUrl;
                contract.Status = ContractStatus.DaXacNhanCSBH.GetEnumDescription();
                contract.UpdatedTime = DateTime.Now;
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
        [SwaggerResponse(StatusCodes.Status200OK, "Thông tin khách hàng đã được lấy thành công.", typeof(CustomerVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng hoặc khách hàng.")]
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
        [SwaggerResponse(StatusCodes.Status200OK, "Xác nhận Phiếu tính giá thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng hoặc khách hàng.")]
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
            contract.UpdatedTime = DateTime.Now;
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

        [HttpPut("check-step-five/{contractId}")]
        [SwaggerOperation(Summary = "Khách hàng nhấn nút Tôi đã thanh toán tiến độ lần 1 ở bước 5")]
        [SwaggerResponse(StatusCodes.Status200OK, "Thanh toán tiến độ lần 1 Hợp đồng mua bán thành công.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Khách hàng chưa upload Ủy nhiệm chi hoặc email không hợp lệ.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng, khách hàng, hoặc chi tiết hợp đồng.")]
        public IActionResult CustomerConfirmUploadPaymentOrder([FromForm] UploadPaymentOrder paymentOrder, Guid contractId)
        {
            if (paymentOrder.RemittanceOrder == null)
            {
                return BadRequest(new
                {
                    message = "Khách hàng chưa upload Ủy nhiệm chi cho đợt thanh toán này."
                });
            }

            var contract = _contractServices.GetContractByID(contractId);
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

            var contractDetails = _contractDetailService.GetContractPaymentDetailByContractID(contract.ContractID);
            if (contractDetails == null)
            {
                return NotFound(new
                {
                    message = "Chi tiết hợp đồng không tồn tại."
                });
            }

            var fistContractDetail = contractDetails.FirstOrDefault();

            string? blobUrl = null;
            if (paymentOrder.RemittanceOrder != null)
            {
                blobUrl = _fileService.UploadSingleImage(paymentOrder.RemittanceOrder, "remittanceorderimage");
            }

            fistContractDetail.RemittanceOrder = blobUrl;
            _contractDetailService.UpdateContractPaymentDetail(fistContractDetail);

            var documentReservation = _documentTemplateService.GetDocumentByDocumentName("Hợp đồng mua bán");
            if (documentReservation == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại"
                });
            }

            contract.DocumentTemplateID = documentReservation.DocumentTemplateID;
            _contractServices.UpdateContract(contract);

            var htmlContent = _contractServices.GenerateDocumentSale(contract.ContractID);
            var pdfBytes = _documentTemplateService.GeneratePdfFromTemplate(htmlContent);
            string? blobUrl1 = null;
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            {
                blobUrl1 = _fileService.UploadSingleFile(pdfStream, contract.DocumentTemplate!.DocumentName, "contractsalefile");
            }

            contract.ContractSaleFile = blobUrl1;
            contract.ContractType = ContractType.MuaBan.GetEnumDescription();
            contract.Status = ContractStatus.DaThanhToanDotMotHDMB.GetEnumDescription();
            _contractServices.UpdateContract(contract);

            //Gửi mail thư mời thanh toán tiền
            Mailrequest mailrequest = new Mailrequest();
            mailrequest.ToEmail = account.Email;
            mailrequest.Subject = "Thông báo mời ký Hợp đồng mua bán";
            mailrequest.Body =
                $"<h5>THÔNG BÁO MỜI KÝ HỢP ĐỒNG MUA BÁN</h5>" +
                $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>" +
                $"<div>Hợp đồng mua bán căn hộ đã được xác lập căn cứ theo lực chọn chính sách bán hàng và phương án thanh toán của Quý khách.</div>" +
                $"<div>Quý khách vui lòng kiểm tra thông tin Hợp đồng mua bán theo đường dẫn dưới đây và ký kết hợp đồng mua bán online.</div>" +
                $"<div>Đường link xem Hợp đồng mua bán</div>" +
                $"<a href='{contract.ContractSaleFile}'>{contract.ContractSaleFile}</a>";

            _emailService.SendEmailAsync(mailrequest);

            return Ok(new
            {
                message = "Thanh toán tiến độ lần 1 Hợp đồng mua bán thành công."
            });

        }

        [HttpPost("step-six-send-otp")]
        [SwaggerOperation(Summary = "Gửi mã OTP qua mail cho khách hàng ở bước 6 khi bấm xác nhận Hợp đồng mua bán")]
        [SwaggerResponse(StatusCodes.Status200OK, "Mã OTP đã được gửi thành công qua email.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng hoặc khách hàng.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Email không hợp lệ hoặc xảy ra lỗi xử lý.")]
        public async Task<IActionResult> SendEmailSale(Guid contractid)
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
                return Ok(new { message = "Mã OTP đã được gửi thành công qua email." });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("step-six-verify-otp")]
        [SwaggerOperation(Summary = "Khách hàng xác nhận mã OTP ở bước 6 cho Hợp đồng mua bán")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xác minh OTP thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng hoặc khách hàng.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "OTP không hợp lệ, đã hết hạn hoặc xảy ra lỗi xử lý.")]
        public IActionResult VerifyOtpSale(Guid contractid, string otp)
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

                        var booking = _bookServices.GetBookingById(contract.BookingID);

                        var propertyId = booking.PropertyID.GetValueOrDefault(Guid.Empty);

                        if (propertyId == Guid.Empty)
                        {
                            throw new ArgumentException("Booking không có căn hộ.");
                        }
                        var property = _propertyServices.GetPropertyById(propertyId);
                        property.Status = PropertyStatus.DaBan.GetEnumDescription();
                        _propertyServices.UpdateProperty(property);

                        //Gửi mail thông báo xán nhận TTDC thành công
                        Mailrequest mailrequest = new Mailrequest();
                        mailrequest.ToEmail = account.Email;
                        mailrequest.Subject = "Xác nhận Hợp đồng mua bán";
                        mailrequest.Body =
                            $"<h5>THÔNG BÁO XÁC NHẬN THÀNH CÔNG HỢP ĐỒNG MUA BÁN</h5>" +
                            $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>" +
                            $"<div>Hợp đồng mua bán của Quý khách đã được xác nhận. Quý khách có thể xem lại thông tin Hợp đồng mua bán</div>" +
                            $"<div>Đường link xem Hợp đồng mua bán</div>" +
                            $"<a href='{contract.ContractSaleFile}'>{contract.ContractSaleFile}</a>";

                        _emailService.SendEmailAsync(mailrequest);

                        contract.Status = ContractStatus.DaXacNhanHDMB.GetEnumDescription();
                        contract.UpdatedTime = DateTime.Now;
                        contract.ExpiredTime = DateTime.Now.AddDays(15);
                        _contractServices.UpdateContract(contract);

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

        [HttpGet("step-seven")]
        [SwaggerOperation(Summary = "Show thông tin lịch lên kí hợp đồng mua bán ở bước 7")]
        [SwaggerResponse(StatusCodes.Status200OK, "Thông tin lịch ký hợp đồng mua bán đã được trả về.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng hoặc chi tiết hợp đồng.")]
        public IActionResult TimeReceiveSaleContract(Guid contractid)
        {
            var contract = _contractServices.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }

            var contractDetails = _contractDetailService.GetContractPaymentDetailByContractID(contractid);
            if (contractDetails == null)
            {
                return NotFound(new
                {
                    message = "Chi tiết hợp đồng không tồn tại."
                });
            }

            var secondContractDetail = contractDetails.Skip(1).FirstOrDefault();
            var DateContractSale = secondContractDetail!.Period;

            return Ok(new
            {
                message = "Quý khách chỉ có thể nhận HĐMB trong khoảng thời gian từ ngày "
                        + DateTime.Now.ToString("yyyy/MM/dd") + " tới ngày "
                        + DateContractSale!.Value.ToString("yyyy/MM/dd")
            });

        }

        [HttpGet("title-contract")]
        [SwaggerOperation(Summary = "Show title Dự án và Căn hộ ở tất cả các bước")]
        [SwaggerResponse(StatusCodes.Status200OK, "Thông tin tiêu đề Dự án và Căn hộ đã được trả về.", typeof(ContractTitleResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng, booking, hoặc căn hộ.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Yêu cầu không hợp lệ.")]
        public IActionResult TitleProjectProperty(Guid contractid)
        {
            var contract = _contractServices.GetContractByID(contractid);
            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Hợp đồng không tồn tại."
                });
            }

            var booking = _bookServices.GetBookingById(contract.BookingID);
            if (booking == null)
            {
                return NotFound(new
                {
                    message = "Booking không tồn tại."
                });
            }

            var propertyId = booking.PropertyID.GetValueOrDefault(Guid.Empty);

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

            var categoryDetail = _projectCategoryDetailServices.GetProjectCategoryDetailByID(booking.ProjectCategoryDetailID);
            var project = _projectServices.GetProjectById(categoryDetail.ProjectID);

            return Ok(new
            {
                property = property.PropertyCode,
                project = project.ProjectName
            });

        }

        [HttpGet("customer-transferred/{contractId}")]
        [SwaggerOperation(Summary = "Hiển thị những Khách hàng được nhận chuyển nhượng")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách khách hàng.", typeof(CustomerVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public IActionResult ShowCustomerTransferred(Guid contractId)
        {
            try
            {
                var contract = _contractServices.GetContractByID(contractId);
                if (contract == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại."
                    });
                }

                var customers = _customerServices.GetAllCustomer();
                var booking = _bookServices.GetBookingById(contract.BookingID);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        message = "Booking không tồn tại."
                    });
                }

                var bookings = _bookServices.CheckCustomerBooking(booking.OpeningForSaleID, booking.ProjectCategoryDetailID, contract.CustomerID);
                var excludedCustomerIds = bookings.Select(b => b.CustomerID).ToHashSet();

                var filteredCustomers = customers.Where(c => !excludedCustomerIds.Contains(c.CustomerID))
                                                 .Where(c => c.CustomerID != contract.CustomerID)
                                                 .Where(c => c.IdentityCardNumber != null)
                                                 .ToList();

                var response = _mapper.Map<List<CustomerVM>>(filteredCustomers);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("check-customer-transferred")]
        [SwaggerOperation(Summary = "Khách hàng nhấn nút Xác nhận khách hàng nhận chuyển nhượng")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xác nhận thông tin khách hàng nhận chuyển nhượng thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public IActionResult ShowContractTransfer(Guid contractId, Guid customerTransfereeId)
        {
            try
            {
                var contract = _contractServices.GetContractByID(contractId);
                if (contract == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại."
                    });
                }

                var customerOne = _customerServices.GetCustomerByID(contract.CustomerID);
                if (customerOne == null)
                {
                    return NotFound(new
                    {
                        message = "Khách hàng chuyển nhượng không tồn tại."
                    });
                }

                var customerTwo = _customerServices.GetCustomerByID(customerTransfereeId);
                if (customerTwo == null)
                {
                    return NotFound(new
                    {
                        message = "Khách hàng nhận chuyển nhượng không tồn tại."
                    });
                }

                var documentReservation = _documentTemplateService.GetDocumentByDocumentName("Thỏa thuận chuyển nhượng");
                if (documentReservation == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại"
                    });
                }

                contract.DocumentTemplateID = documentReservation.DocumentTemplateID;
                contract.Status = ContractStatus.ChoXacNhanChuyenNhuong.GetEnumDescription();
                contract.UpdatedTime = DateTime.Now;
                _contractServices.UpdateContract(contract);

                var htmlContent = _contractServices.GenerateDocumentTransfer(contract.ContractID, customerTransfereeId);
                var pdfBytes = _documentTemplateService.GeneratePdfFromTemplate(htmlContent);
                string? blobUrl = null;
                using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
                {
                    blobUrl = _fileService.UploadSingleFile(pdfStream, customerTransfereeId.ToString(), "contractdepositfile");
                }

                contract.ContractTransferFile = blobUrl;
                _contractServices.UpdateContract(contract);

                return Ok(new
                {
                    message = "Xác nhận thông tin khách hàng nhận chuyển nhượng thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("check-transfer-send-otp")]
        [SwaggerOperation(Summary = "Gửi mã OTP qua mail cho khách hàng xác nhận TTCN")]
        [SwaggerResponse(StatusCodes.Status200OK, "Mã OTP đã được gửi thành công qua email.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public async Task<IActionResult> SendEmailTransfer(Guid contractid)
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
                return Ok(new { message = "Mã OTP đã được gửi thành công qua email." });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("check-transfer-verify-otp")]
        [SwaggerOperation(Summary = "Khách hàng xác nhận mã OTP ở bước xác nhận TTCN")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xác minh OTP thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public IActionResult VerifyOtpTransfer(Guid contractid, string otp)
        {
            try
            {
                var contract = _contractServices.GetContractByID(contractid);
                if (contract == null)
                {
                    return NotFound(new { message = "Hợp đồng không tồn tại." });
                }
                var customerOne = _customerServices.GetCustomerByID(contract.CustomerID);
                if (customerOne == null)
                {
                    return NotFound(new { message = "Khách hàng không tồn tại." });
                }
                var accountOne = _accountService.GetAccountByID(customerOne.AccountID);

                if (accountOne == null || string.IsNullOrEmpty(accountOne.Email) || !IsValidEmail(accountOne.Email))
                {
                    return BadRequest(new { message = "Địa chỉ Email không hợp lệ." });
                }
                if (string.IsNullOrEmpty(accountOne.Email) || string.IsNullOrEmpty(otp))
                {
                    return BadRequest(new { message = "Email and OTP là bắt buộc." });
                }
                if (otpStorage.TryGetValue(accountOne.Email, out var otpEntry))
                {

                    if (otpEntry.Otp == otp && otpEntry.Expiration > DateTime.UtcNow)
                    {

                        otpStorage.Remove(accountOne.Email);

                        if (contract.ContractTransferFile == null)
                        {
                            return BadRequest(new { message = "Hợp đồng không có thỏa thuận chuyển nhượng." });
                        }

                        var extraction = new CustomerIdExtraction();

                        string customerTransfereeStringId = extraction.ExtractCustomerIdFromUrl(contract.ContractTransferFile);
                        Guid customerTransfereeId = Guid.Parse(customerTransfereeStringId);

                        var customerTwo = _customerServices.GetCustomerByID(customerTransfereeId);
                        if (customerTwo == null)
                        {
                            return NotFound(new
                            {
                                message = "Khách hàng nhận chuyển nhượng không tồn tại."
                            });
                        }
                        var accountTwo = _accountService.GetAccountByID(customerTwo.AccountID);

                        if (accountTwo == null || string.IsNullOrEmpty(accountTwo.Email) || !IsValidEmail(accountTwo.Email))
                        {
                            return BadRequest(new { message = "Địa chỉ Email không hợp lệ." });
                        }

                        //Gửi mail thông báo xán nhận TTCN thành công cho KH chuyển nhượng
                        Mailrequest mailrequestOne = new Mailrequest();
                        mailrequestOne.ToEmail = accountOne.Email;
                        mailrequestOne.Subject = "Xác nhận Thỏa thuận chuyển nhượng";
                        mailrequestOne.Body =
                            $"<h5>THÔNG BÁO XÁC NHẬN CHUYỂN NHƯỢNG THỎA THUẬN ĐẶT CỌC THÀNH CÔNG</h5>" +
                            $"<div>Kính gửi quý khách {customerOne.FullName}</div>" +
                            $"<div>Thảo thuận chuyển nhượng của Quý khách đã được xác nhận. Quý khách có thể xem lại thông tin Thỏa thuận chuyển nhượng.</div>" +
                            $"<div>Đường link xem Thỏa thuận chuyển nhượng</div>" +
                            $"<a href='{contract.ContractTransferFile}'>{contract.ContractTransferFile}</a>";

                        _emailService.SendEmailAsync(mailrequestOne);

                        //Gửi mail thông báo xán nhận TTCN thành công cho KH nhận chuyển nhượng
                        Mailrequest mailrequestTwo = new Mailrequest();
                        mailrequestTwo.ToEmail = accountTwo.Email;
                        mailrequestTwo.Subject = "Yêu Cầu Xác Nhận Thỏa Thuận Chuyển Nhượng";
                        mailrequestTwo.Body =
                            $"<h5>THÔNG BÁO YÊU CẦU XÁC NHẬN CHUYỂN NHƯỢNG THỎA THUẬN ĐẶT CỌC</h5>" +
                            $"<div>Kính gửi quý khách {customerTwo.FullName}</div>" +
                            $"<div>Quý khách vui lòng kiểm tra và xác nhận Thỏa thuận chuyển nhượng thỏa thuận đặt cọc.</div>" +
                            $"<div>Đường link xem Thỏa thuận chuyển nhượng</div>" +
                            $"<a href='{contract.ContractTransferFile}'>{contract.ContractTransferFile}</a>";

                        _emailService.SendEmailAsync(mailrequestTwo);

                        contract.Status = ContractStatus.DaXacNhanChuyenNhuong.GetEnumDescription();
                        contract.ContractType = ContractType.ChuyenNhuong.GetEnumDescription();
                        contract.UpdatedTime = DateTime.Now;
                        contract.ExpiredTime = DateTime.Now.AddDays(1);
                        _contractServices.UpdateContract(contract);

                        string nextContractCode = GenerateNextContractCode();

                        var newContract = new ContractCreateDTO
                        {
                            ContractID = Guid.NewGuid(),
                            ContractCode = nextContractCode,
                            ContractType = ContractType.DatCoc.GetEnumDescription(),
                            CreatedTime = DateTime.Now,
                            UpdatedTime = null,
                            ExpiredTime = DateTime.Now.AddDays(1),
                            TotalPrice = contract.TotalPrice,
                            Description = "Đây là hợp đồng của khách hàng nhận chuyển nhượng",
                            ContractDepositFile = null,
                            ContractSaleFile = null,
                            PriceSheetFile = null,
                            ContractTransferFile = null,
                            Status = ContractStatus.ChoXacNhanTTCNTTDC.GetEnumDescription(),
                            DocumentTemplateID = contract.DocumentTemplateID,
                            BookingID = contract.BookingID,
                            CustomerID = customerTransfereeId,
                            PaymentProcessID = null,
                            PromotionDetailID = null
                        };

                        var _contract = _mapper.Map<RealEstateProjectSaleBusinessObject.BusinessObject.Contract>(newContract);
                        _contract.ContractDepositFile = contract.ContractTransferFile;
                        _contractServices.AddNewContract(_contract);

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

        [HttpPut("check-receive-transfer")]
        [SwaggerOperation(Summary = "Khách hàng nhận chuyển nhượng nhấn nút Xác nhận TTCNTTDC")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xác nhận thỏa thuận chuyển nhượng thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
        public IActionResult CustomerConfirmContractTransfer(Guid contractid)
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

            contract.Status = ContractStatus.DaXacNhanTTDC.GetEnumDescription();
            contract.UpdatedTime = DateTime.Now;
            contract.ExpiredTime = DateTime.Now.AddDays(1);
            _contractServices.UpdateContract(contract);

            //Gửi mail thư mời thanh toán tiền
            Mailrequest mailrequest = new Mailrequest();
            mailrequest.ToEmail = account.Email;
            mailrequest.Subject = "Xác nhận Thỏa thuận chuyển nhượng";
            mailrequest.Body =
                $"<h5>THÔNG BÁO XÁC NHẬN CHUYỂN NHƯỢNG THỎA THUẬN ĐẶT CỌC THÀNH CÔNG</h5>" +
                $"<div>Kính gửi quý khách {contract.Customer.FullName}</div>" +
                $"<div>Thảo thuận chuyển nhượng của Quý khách đã được xác nhận. Quý khách có thể xem lại thông tin Thỏa thuận chuyển nhượng.</div>" +
                $"<div>Đường link xem Thỏa thuận chuyển nhượng</div>" +
                $"<a href='{contract.ContractDepositFile}'>{contract.ContractDepositFile}</a>";

            _emailService.SendEmailAsync(mailrequest);

            return Ok(new
            {
                message = "Xác nhận thỏa thuận chuyển nhượng thành công."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Contract")]
        [SwaggerResponse(StatusCodes.Status200OK, "Hợp đồng đã được tạo thành công.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Yêu cầu không hợp lệ hoặc xảy ra lỗi xử lý.")]
        public IActionResult AddNewContract(ContractCreateDTO contract)
        {
            try
            {
                var booking = _bookServices.GetBookingById(contract.BookingID);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        message = "Booking không tồn tại."
                    });
                }

                string nextContractCode = GenerateNextContractCode();

                var documentReservation = _documentTemplateService.GetDocumentByDocumentName("Thỏa thuận đặt cọc");
                if (documentReservation == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại"
                    });
                }

                var newContract = new ContractCreateDTO
                {
                    ContractID = Guid.NewGuid(),
                    ContractCode = nextContractCode,
                    ContractType = ContractType.DatCoc.GetEnumDescription(),
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    ExpiredTime = DateTime.Now.AddDays(1),
                    TotalPrice = contract.TotalPrice,
                    Description = contract.Description,
                    ContractDepositFile = null,
                    ContractSaleFile = null,
                    PriceSheetFile = null,
                    ContractTransferFile = null,
                    Status = ContractStatus.ChoXacNhanTTGD.GetEnumDescription(),
                    DocumentTemplateID = documentReservation.DocumentTemplateID,
                    BookingID = contract.BookingID,
                    CustomerID = booking.CustomerID,
                    PaymentProcessID = null,
                    PromotionDetailID = null
                };

                var _contract = _mapper.Map<RealEstateProjectSaleBusinessObject.BusinessObject.Contract>(newContract);
                _contractServices.AddNewContract(_contract);

                return Ok(new
                {
                    message = "Tạo hợp đồng thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Contract by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật hợp đồng thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Không tìm thấy hợp đồng.")]
        public IActionResult UpdateContract([FromForm] ContractUpdateDTO contract, Guid id)
        {
            try
            {
                string? blobUrl1 = null, blobUrl2 = null, blobUrl3 = null, blobUrl4 = null;
                var depositFile = contract.ContractDepositFile;
                var saleFile = contract.ContractSaleFile;
                var priceFile = contract.PriceSheetFile;
                var transferFile = contract.ContractTransferFile;
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
                    using (var pdfPriceStream = priceFile.OpenReadStream())
                    {
                        blobUrl3 = _fileService.UploadSingleFile(pdfPriceStream, priceFile.FileName, "pricefile");
                    }
                }
                if (transferFile != null)
                {
                    using (var pdfTransferStream = transferFile.OpenReadStream())
                    {
                        blobUrl4 = _fileService.UploadSingleFile(pdfTransferStream, transferFile.FileName, "contracttransferfile");
                    }
                }

                var existingContract = _contractServices.GetContractByID(id);
                if (existingContract != null)
                {
                    if (!string.IsNullOrEmpty(contract.ContractType))
                    {
                        existingContract.ContractType = contract.ContractType;
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
                    if (!string.IsNullOrEmpty(contract.Status) && int.TryParse(contract.Status, out int statusValue))
                    {
                        if (Enum.IsDefined(typeof(ContractStatus), statusValue))
                        {
                            var statusEnum = (ContractStatus)statusValue;
                            var statusDescription = statusEnum.GetEnumDescription();
                            existingContract.Status = statusDescription;
                        }
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
                    if (blobUrl4 != null)
                    {
                        existingContract.ContractTransferFile = blobUrl4;
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
        [SwaggerResponse(StatusCodes.Status200OK, "Xóa hợp đồng thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Hợp đồng không tồn tại.")]
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
            string nextContractCode = nextNumber.ToString() + "/HD";

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
