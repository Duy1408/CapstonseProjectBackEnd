using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Stripe;
using Stripe.Issuing;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.ContractPaymentDetailController
{
    [Route("api/contract-payment-details")]
    [ApiController]
    public class ContractPaymentDetailController : ControllerBase
    {
        private readonly IContractPaymentDetailServices _detailService;
        private readonly IContractServices _contractService;
        private readonly IBookingServices _bookingService;
        private readonly IPropertyServices _propertyService;
        private readonly IFileUploadToBlobService _fileService;
        private readonly IMapper _mapper;

        public ContractPaymentDetailController(IContractPaymentDetailServices detailService,
                    IFileUploadToBlobService fileService, IMapper mapper, IContractServices contractService,
                    IBookingServices bookingService, IPropertyServices propertyService)
        {
            _detailService = detailService;
            _contractService = contractService;
            _bookingService = bookingService;
            _propertyService = propertyService;
            _fileService = fileService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get All ContractPaymentDetail")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Chi tiết hợp đồng.", typeof(List<ContractPaymentDetailVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Chi tiết hợp đồng không tồn tại.")]
        public IActionResult GetAllContractPaymentDetail()
        {
            try
            {
                if (_detailService.GetAllContractPaymentDetail() == null)
                {
                    return NotFound(new
                    {
                        message = "Chi tiết hợp đồng không tồn tại."
                    });
                }
                var details = _detailService.GetAllContractPaymentDetail();
                var response = _mapper.Map<List<ContractPaymentDetailVM>>(details);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get ContractPaymentDetail By ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về thông tin Chi tiết hợp đồng.", typeof(ContractPaymentDetailVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Chi tiết hợp đồng không tồn tại.")]
        public IActionResult GetContractPaymentDetailByID(Guid id)
        {
            var detail = _detailService.GetContractPaymentDetailByID(id);

            if (detail != null)
            {
                var responese = _mapper.Map<ContractPaymentDetailVM>(detail);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chi tiết hợp đồng không tồn tại."
            });

        }

        [HttpGet("contract/{contractId}")]
        [SwaggerOperation(Summary = "Get ContractPaymentDetail By ContractID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Chi tiết hợp đồng.", typeof(List<ContractPaymentDetailVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Chi tiết hợp đồng không tồn tại.")]
        public IActionResult GetContractPaymentDetailByContractID(Guid contractId)
        {
            var detail = _detailService.GetContractPaymentDetailByContractID(contractId);

            if (detail != null)
            {
                var details = _mapper.Map<List<ContractPaymentDetailVM>>(detail);

                var responese = details.Select(detailContract => new ContractPaymentDetailVM
                {
                    ContractPaymentDetailID = detailContract.ContractPaymentDetailID,
                    PaymentRate = detailContract.PaymentRate,
                    Description = detailContract.Description,
                    Period = detailContract.Period,
                    PaidValue = detailContract.PaidValue,
                    RemittanceOrder = detailContract.RemittanceOrder,
                    Status = detailContract.Status,
                    ContractID = detailContract.ContractID,
                    ContractCode = detailContract.ContractCode,
                    PaymentPolicyID = detailContract.PaymentPolicyID,
                    PaymentPolicyName = detailContract.PaymentPolicyName,
                    PaidValueLate = _detailService.CalculateLatePaymentInterest(detailContract.ContractPaymentDetailID)
                }).ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chi tiết hợp đồng không tồn tại."
            });

        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new ContractPaymentDetail")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tạo chi tiết hợp đồng thành công.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Yêu cầu không hợp lệ hoặc xảy ra lỗi xử lý.")]
        public IActionResult AddNewContractPaymentDetail(ContractPaymentDetailCreateDTO detail)
        {
            try
            {

                var newDetail = new ContractPaymentDetailCreateDTO
                {
                    ContractPaymentDetailID = Guid.NewGuid(),
                    PaymentRate = detail.PaymentRate,
                    Description = detail.Description,
                    Period = detail.Period,
                    PaidValue = detail.PaidValue,
                    PaidValueLate = detail.PaidValue,
                    RemittanceOrder = null,
                    Status = false,
                    ContractID = detail.ContractID,
                    PaymentPolicyID = detail.PaymentPolicyID,
                };

                var _detail = _mapper.Map<ContractPaymentDetail>(newDetail);
                _detailService.AddNewContractPaymentDetail(_detail);

                return Ok(new
                {
                    message = "Tạo chi tiết hợp đồng thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update ContractPaymentDetail by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật Chi tiết hợp đồng thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Chi tiết hợp đồng không tồn tại.")]
        public IActionResult UpdateContractPaymentDetail([FromForm] ContractPaymentDetailUpdateDTO detail, Guid id)
        {
            try
            {
                string? blobUrl = null;
                if (detail.RemittanceOrder != null)
                {
                    blobUrl = _fileService.UploadSingleImage(detail.RemittanceOrder, "remittanceorderimage");
                }

                var existingDetail = _detailService.GetContractPaymentDetailByID(id);
                if (existingDetail != null)
                {

                    if (detail.PaymentRate.HasValue)
                    {
                        existingDetail.PaymentRate = detail.PaymentRate.Value;
                    }
                    if (!string.IsNullOrEmpty(detail.Description))
                    {
                        existingDetail.Description = detail.Description;
                    }
                    if (detail.Period.HasValue)
                    {
                        existingDetail.Period = detail.Period.Value;
                    }
                    if (detail.PaidValue.HasValue)
                    {
                        existingDetail.PaidValue = detail.PaidValue.Value;
                    }
                    if (detail.PaidValueLate.HasValue)
                    {
                        existingDetail.PaidValueLate = detail.PaidValueLate.Value;
                    }
                    if (detail.Status.HasValue)
                    {
                        existingDetail.Status = detail.Status.Value;
                    }
                    if (detail.ContractID.HasValue)
                    {
                        existingDetail.ContractID = detail.ContractID.Value;
                    }
                    if (detail.PaymentPolicyID.HasValue)
                    {
                        existingDetail.PaymentPolicyID = detail.PaymentPolicyID.Value;
                    }
                    if (blobUrl != null)
                    {
                        existingDetail.RemittanceOrder = blobUrl;
                    }

                    _detailService.UpdateContractPaymentDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Cập nhật Chi tiết hợp đồng thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Chi tiết hợp đồng không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("confirm/{id}")]
        [SwaggerOperation(Summary = "Staff Confirm PaymentOrder By ContractPaymentDetailID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Staff xác nhận Ủy nhiệm chi thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Chi tiết hợp đồng không tồn tại.")]
        public IActionResult StaffConfirmPaymentOrder(Guid id)
        {
            try
            {
                var contractDetail = _detailService.GetContractPaymentDetailByID(id);
                if (contractDetail == null)
                {
                    return NotFound(new
                    {
                        message = "Chi tiết hợp đồng không tồn tại."
                    });
                }

                if (contractDetail.RemittanceOrder == null)
                {
                    return BadRequest(new
                    {
                        message = "Khách hàng chưa upload Ủy nhiệm chi cho đợt thanh toán này."
                    });
                }

                var paymentDetails = _detailService.GetContractPaymentDetailByContractID(contractDetail.ContractID);
                if (paymentDetails == null || !paymentDetails.Any())
                {
                    return NotFound(new
                    {
                        message = "Chi tiết hợp đồng không tồn tại."
                    });
                }

                //đợt hiện tại của id
                var currentPaymentRate = contractDetail.PaymentRate;

                //đợt thanh toán trước
                var previousPaymentDetail = paymentDetails.FirstOrDefault(d => d.PaymentRate == currentPaymentRate - 1);
                if (previousPaymentDetail != null && previousPaymentDetail.Status == false)
                {
                    return BadRequest(new
                    {
                        message = "Đợt thanh toán trước đó chưa được xác nhận bởi Staff."
                    });
                }


                //đợt thanh toán cuối cùng
                var lastPaymentDetail = paymentDetails.OrderByDescending(d => d.PaymentRate)
                                                      .FirstOrDefault();
                if (lastPaymentDetail == null)
                {
                    return NotFound(new
                    {
                        message = "Chi tiết hợp đồng cuối cùng không tồn tại."
                    });
                }
                if (lastPaymentDetail.ContractPaymentDetailID == id)
                {
                    contractDetail.Status = true;
                    _detailService.UpdateContractPaymentDetail(contractDetail);

                    var contract = _contractService.GetContractByID(contractDetail.ContractID);
                    if (contract == null)
                    {
                        return NotFound(new
                        {
                            message = "Hợp đồng không tồn tại."
                        });
                    }
                    contract.Status = ContractStatus.DaBanGiaoQSHD.GetEnumDescription();
                    contract.UpdatedTime = DateTime.Now;
                    contract.ExpiredTime = DateTime.Now;
                    _contractService.UpdateContract(contract);

                    var booking = _bookingService.GetBookingById(contract.BookingID);
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
                    var property = _propertyService.GetPropertyById(propertyId);
                    if (booking == null)
                    {
                        return NotFound(new
                        {
                            message = "Booking không tồn tại."
                        });
                    }
                    property.Status = PropertyStatus.BanGiao.GetEnumDescription();
                    _propertyService.UpdateProperty(property);

                    return Ok(new
                    {
                        message = "Staff xác nhận Ủy nhiệm chi thành công."
                    });

                }

                contractDetail.Status = true;
                _detailService.UpdateContractPaymentDetail(contractDetail);

                return Ok(new
                {
                    message = "Staff xác nhận Ủy nhiệm chi thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("upload-payment-order/{id}")]
        [SwaggerOperation(Summary = "Customer Upload PaymentOrder By ContractPaymentDetailID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Customer tải lên Ủy nhiệm chi thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Chi tiết hợp đồng không tồn tại.")]
        public IActionResult CustomerUploadPaymentOrder([FromForm] UploadPaymentOrder paymentOrder, Guid id)
        {
            try
            {
                if (paymentOrder.RemittanceOrder == null)
                {
                    return BadRequest(new
                    {
                        message = "Khách hàng chưa upload Ủy nhiệm chi cho đợt thanh toán này."
                    });
                }

                var contractDetail = _detailService.GetContractPaymentDetailByID(id);
                if (contractDetail == null)
                {
                    return NotFound(new
                    {
                        message = "Chi tiết hợp đồng không tồn tại."
                    });
                }

                var paymentDetails = _detailService.GetContractPaymentDetailByContractID(contractDetail.ContractID);
                if (paymentDetails == null || !paymentDetails.Any())
                {
                    return NotFound(new
                    {
                        message = "Chi tiết hợp đồng không tồn tại."
                    });
                }

                //đợt hiện tại của id
                var currentPaymentRate = contractDetail.PaymentRate;

                //đợt thanh toán trước
                var previousPaymentDetail = paymentDetails.FirstOrDefault(d => d.PaymentRate == currentPaymentRate - 1);
                if (previousPaymentDetail != null && previousPaymentDetail.Status == false)
                {
                    return BadRequest(new
                    {
                        message = "Đợt thanh toán trước đó chưa được xác nhận bởi Staff."
                    });
                }

                if (contractDetail.Status == true)
                {
                    return BadRequest(new
                    {
                        message = "Staff đã xác nhận Ủy nhiệm chi của đợt thanh toán này."
                    });
                }

                string? blobUrl = null;
                if (paymentOrder.RemittanceOrder != null)
                {
                    blobUrl = _fileService.UploadSingleImage(paymentOrder.RemittanceOrder, "remittanceorderimage");
                }

                contractDetail.RemittanceOrder = blobUrl;
                _detailService.UpdateContractPaymentDetail(contractDetail);

                return Ok(new
                {
                    message = "Customer tải lên Ủy nhiệm chi thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete ContractPaymentDetail by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xóa chi tiết hợp đồng thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Chi tiết hợp đồng không tồn tại.")]
        public IActionResult DeleteContractPaymentDetailByID(Guid id)
        {
            try
            {
                var detail = _detailService.GetContractPaymentDetailByID(id);
                if (detail != null)
                {
                    _detailService.DeleteContractPaymentDetailByID(id);
                    return Ok(new
                    {
                        message = "Xóa chi tiết hợp đồng thành công."
                    });
                }

                return NotFound(new
                {
                    message = "Chi tiết hợp đồng không tồn tại."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
