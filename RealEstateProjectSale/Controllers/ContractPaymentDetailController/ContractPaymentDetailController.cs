using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.ContractPaymentDetailController
{
    [Route("api/contract-payment-details")]
    [ApiController]
    public class ContractPaymentDetailController : ControllerBase
    {
        private readonly IContractPaymentDetailServices _detailService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IMapper _mapper;

        public ContractPaymentDetailController(IContractPaymentDetailServices detailService, BlobServiceClient blobServiceClient, IMapper mapper)
        {
            _detailService = detailService;
            _blobServiceClient = blobServiceClient;
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
                        message = "ContractPaymentDetail not found."
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
                message = "ContractPaymentDetail not found."
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
                    DetailName = detail.DetailName,
                    CreatedTime = DateTime.Now,
                    PaymentRate = detail.PaymentRate,
                    Amountpaid = detail.Amountpaid,
                    TaxRate = detail.TaxRate,
                    MoneyTax = detail.MoneyTax,
                    MoneyReceived = detail.MoneyReceived,
                    NumberDayLate = detail.NumberDayLate,
                    InterestRate = detail.InterestRate,
                    MoneyInterestRate = detail.MoneyInterestRate,
                    MoneyExist = detail.MoneyExist,
                    Description = detail.Description,
                    RemittanceOrder = null,
                    ContractID = detail.ContractID,
                };

                var _detail = _mapper.Map<ContractPaymentDetail>(newDetail);
                _detailService.AddNewContractPaymentDetail(_detail);

                return Ok(new
                {
                    message = "Create ContractPaymentDetail Successfully"
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
                var containerInstance = _blobServiceClient.GetBlobContainerClient("remittanceorderimage");
                string? blobUrl = null;
                if (detail.RemittanceOrder != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{detail.RemittanceOrder.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(detail.RemittanceOrder.OpenReadStream());
                    var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/remittanceorderimage";
                    blobUrl = $"{storageAccountUrl}/{blobName}";
                }

                var existingDetail = _detailService.GetContractPaymentDetailByID(id);
                if (existingDetail != null)
                {

                    if (!string.IsNullOrEmpty(detail.DetailName))
                    {
                        existingDetail.DetailName = detail.DetailName;
                    }
                    if (detail.PaymentRate.HasValue)
                    {
                        existingDetail.PaymentRate = detail.PaymentRate.Value;
                    }
                    if (detail.Amountpaid.HasValue)
                    {
                        existingDetail.Amountpaid = detail.Amountpaid.Value;
                    }
                    if (detail.TaxRate.HasValue)
                    {
                        existingDetail.TaxRate = detail.TaxRate.Value;
                    }
                    if (detail.MoneyTax.HasValue)
                    {
                        existingDetail.MoneyTax = detail.MoneyTax.Value;
                    }
                    if (detail.MoneyReceived.HasValue)
                    {
                        existingDetail.MoneyReceived = detail.MoneyReceived.Value;
                    }
                    if (detail.NumberDayLate.HasValue)
                    {
                        existingDetail.NumberDayLate = detail.NumberDayLate.Value;
                    }
                    if (detail.InterestRate.HasValue)
                    {
                        existingDetail.InterestRate = detail.InterestRate.Value;
                    }
                    if (detail.MoneyInterestRate.HasValue)
                    {
                        existingDetail.MoneyInterestRate = detail.MoneyInterestRate.Value;
                    }
                    if (detail.MoneyExist.HasValue)
                    {
                        existingDetail.MoneyExist = detail.MoneyExist.Value;
                    }
                    if (!string.IsNullOrEmpty(detail.Description))
                    {
                        existingDetail.Description = detail.Description;
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
                    message = "ContractPaymentDetail not found."
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
                        message = "Delete ContractPaymentDetail Successfully"
                    });
                }

                return NotFound(new
                {
                    message = "ContractPaymentDetail not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
