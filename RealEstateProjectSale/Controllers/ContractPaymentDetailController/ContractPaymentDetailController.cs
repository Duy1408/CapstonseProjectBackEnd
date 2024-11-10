using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Stripe;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.ContractPaymentDetailController
{
    [Route("api/contract-payment-details")]
    [ApiController]
    public class ContractPaymentDetailController : ControllerBase
    {
        private readonly IContractPaymentDetailServices _detailService;
        private readonly IFileUploadToBlobService _fileService;
        private readonly IMapper _mapper;

        public ContractPaymentDetailController(IContractPaymentDetailServices detailService,
                    IFileUploadToBlobService fileService, IMapper mapper)
        {
            _detailService = detailService;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All ContractPaymentDetail")]
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get ContractPaymentDetail By ID")]
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

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new ContractPaymentDetail")]
        public IActionResult AddNewContractPaymentDetail(ContractPaymentDetailCreateDTO detail)
        {
            try
            {

                var newDetail = new ContractPaymentDetailCreateDTO
                {
                    ContractPaymentDetailID = Guid.NewGuid(),
                    PaymentRate = detail.PaymentRate,              
                    Description = detail.Description,
                    Period=detail.Period,
                    PaidValue = detail.PaidValue,                 
                    PaidValueLate = detail.PaidValue,
                    RemittanceOrder = null,
                    Status = false,
                    ContractID = detail.ContractID,
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

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update ContractPaymentDetail by ID")]
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
                    if (blobUrl != null)
                    {
                        existingDetail.RemittanceOrder = blobUrl;
                    }

                    _detailService.UpdateContractPaymentDetail(existingDetail);

                    return Ok(new
                    {
                        message = "Update ContractPaymentDetail Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Cập nhật chi tiết hợp đồng thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete ContractPaymentDetail by ID")]
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
