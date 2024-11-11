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
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

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

        public ContractController(IContractServices contractServices, IBookingServices bookServices,
                IFileUploadToBlobService fileService, IMapper mapper, IPromotionDetailServices promotiondetail, IPaymentProcessServices paymentprocess,
                ICustomerServices customerServices, IPropertyServices propertyServices
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

        [HttpGet("stepone")]
        [SwaggerOperation(Summary = "Get Contract by customer ID")]
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
            var property = _propertyServices.GetPropertyById(book.PropertyID);
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
           property= propertyresponese,
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
                    Status = ContractStatus.ChoXacNhanTTDC.GetEnumDescription(),
                    DocumentTemplateID = contract.DocumentTemplateID,
                    BookingID = contract.BookingID,
                    CustomerID = contract.CustomerID,
                    PaymentProcessID = contract.PaymentProcessID,
                    PromotionDetailID = contract.PromotionDetailID

                };

                var _contract = _mapper.Map<Contract>(newContract);
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

        [HttpPut("stepthree")]
        [SwaggerOperation(Summary = "Update Contract chose promotion detail")]
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

                contract.PaymentProcessID = paymentprocessid;
                contract.PromotionDetailID = promotiondetailid;
                _contractServices.UpdateContract(contract);





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


    }
}
