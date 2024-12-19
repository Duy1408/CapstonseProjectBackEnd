using AutoMapper;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Stripe;
using Stripe.FinancialConnections;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.Contracts;


namespace RealEstateProjectSale.Controllers.ContractHistoryController
{
    [Route("api/contracthistorys")]
    [ApiController]
    public class ContractHistoryController : ControllerBase
    {
        private readonly IContractHistoryServices _contractHistoryService;
        private readonly IContractServices _contractService;
        private readonly ICustomerServices _customerService;
        private readonly IMapper _mapper;
        private readonly IFileUploadToBlobService _file;
        public ContractHistoryController(IContractHistoryServices contractHistoryService, IMapper mapper, IFileUploadToBlobService file,
            IContractServices contractService, ICustomerServices customerService)
        {
            _contractHistoryService = contractHistoryService;
            _contractService = contractService;
            _mapper = mapper;
            _file = file;
            _customerService = customerService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all ContractHistory")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách ContractHistory.", typeof(List<ContractHistoryVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Contract History không tồn tại.")]
        public IActionResult GetAllContractHistory()
        {
            try
            {
                if (_contractHistoryService.GetContractHistorys() == null)
                {
                    return NotFound(new
                    {
                        message = "Lịch sử chuyển nhượng không tồn tại."
                    });
                }
                var contracthistorys = _contractHistoryService.GetContractHistorys();
                var response = _mapper.Map<List<ContractHistoryVM>>(contracthistorys);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get ContractHistory By ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về thông tin ContractHistory.", typeof(ContractHistoryVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "ContractHistory không tồn tại.")]
        public IActionResult GetContractHistoryByID(Guid id)
        {
            var contracthistory = _contractHistoryService.GetContractHistoryById(id);

            if (contracthistory != null)
            {
                var responese = _mapper.Map<ContractHistoryVM>(contracthistory);
                return Ok(responese);
            }
            return NotFound(new
            {
                message = "Lịch sử chuyển nhượng không tồn tại."
            });
        }

        [HttpGet("contract/{contractId}")]
        [SwaggerOperation(Summary = "Get ContractHistory By contractID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về thông tin ContractHistory.", typeof(ContractHistoryVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "ContractHistory không tồn tại.")]
        public IActionResult GetContractHistoryByContracyID(Guid contractId)
        {
            var contracthistory = _contractHistoryService.GetContractHistoryByContractID(contractId);

            if (contracthistory != null)
            {
                var responese = _mapper.Map<List<ContractHistoryVM>>(contracthistory);
                return Ok(responese);
            }
            return NotFound(new
            {
                message = "Lịch sử chuyển nhượng không tồn tại."
            });
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete contract history")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xóa contract history thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "contract history không tồn tại.")]
        public IActionResult DeleteContractHistory(Guid id)
        {

            var contracthistory = _contractHistoryService.GetContractHistoryById(id);
            if (contracthistory == null)
            {
                return NotFound(new
                {
                    message = "Lịch sử chuyển nhượng không tồn tại."
                });
            }

            _contractHistoryService.DeleteContractHistory(id);

            return Ok(new
            {
                message = "Xóa lịch sử chuyển nhượn thành công."
            });
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Contract History")]
        public IActionResult AddNewContractHistory([FromForm] ContractHistoryRequestDTO history)
        {
            try
            {
                var existCode = _contractHistoryService.CheckNotarizedContractCode(history.NotarizedContractCode);
                if (existCode != null)
                {
                    return BadRequest(new
                    {
                        message = "Mã hợp đồng công chứng đã tồn tại."
                    });
                }

                var contract = _contractService.GetContractByID(history.ContractID);
                if (contract == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại."
                    });
                }

                if (contract.Status != ContractStatus.DaXacNhanHDMB.GetEnumDescription() && contract.Status != ContractStatus.DaBanGiaoQSHD.GetEnumDescription())
                {
                    return BadRequest(new
                    {
                        message = "Khách hàng chưa xác nhận hợp đồng mua bán."
                    });
                }

                if (contract.CustomerID == history.CustomerID)
                {
                    return BadRequest(new
                    {
                        message = "Khách hàng này không được nhận chuyển nhượng."
                    });
                }

                var existingIdentification = _customerService.CheckCustomerByIdentification(history.CustomerID);
                if (existingIdentification != null)
                {
                    return NotFound(new
                    {
                        message = "Khách hàng nhận chuyển nhượng chưa cập nhật giấy tờ tùy thân."
                    });
                }

                string? blobUrl = null;
                var attachFile = history.AttachFile;
                if (attachFile != null)
                {
                    using (var pdfStream = attachFile.OpenReadStream())
                    {
                        blobUrl = _file.UploadSingleFile(pdfStream, attachFile.FileName, "attachfile");
                    }
                }

                var newContractHistory = new ContractHistoryCreateDTO
                {
                    ContractHistoryID = Guid.NewGuid(),
                    NotarizedContractCode = history.NotarizedContractCode,
                    Note = history.Note,
                    AttachFile = history.AttachFile,
                    CreatedTime = DateTime.Now,
                    CustomerID = contract.CustomerID,
                    ContractID = history.ContractID
                };

                var contracthistory = _mapper.Map<ContractHistory>(newContractHistory);
                contracthistory.AttachFile = blobUrl;
                _contractHistoryService.AddNewContractHistory(contracthistory);

                contract.CustomerID = history.CustomerID;
                contract.UpdatedTime = DateTime.Now;
                _contractService.UpdateContract(contract);

                return Ok(new
                {
                    message = "Chuyển nhượng hợp đồng thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update contracthistory by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật contracthistory thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Contracthistory không tồn tại.")]
        public IActionResult UpdateBooking([FromForm] ContractHistoryUpdateDTO history, Guid id)
        {
            try
            {
                string? blobUrl = null;
                var attachFile = history.AttachFile;
                if (attachFile != null)
                {
                    using (var pdfStream = attachFile.OpenReadStream())
                    {
                        blobUrl = _file.UploadSingleFile(pdfStream, attachFile.FileName, "attachfile");
                    }
                }

                var existingHistory = _contractHistoryService.GetContractHistoryById(id);
                if (existingHistory != null)
                {
                    if (!string.IsNullOrEmpty(history.NotarizedContractCode))
                    {
                        existingHistory.NotarizedContractCode = history.NotarizedContractCode;
                    }
                    if (!string.IsNullOrEmpty(history.Note))
                    {
                        existingHistory.Note = history.Note;
                    }
                    if (blobUrl != null)
                    {
                        existingHistory.AttachFile = blobUrl;
                    }
                    if (history.CustomerID.HasValue)
                    {
                        existingHistory.CustomerID = history.CustomerID.Value;
                    }
                    if (history.CustomerID.HasValue)
                    {
                        existingHistory.CustomerID = history.CustomerID.Value;
                    }
                    if (history.ContractID.HasValue)
                    {
                        existingHistory.ContractID = history.ContractID.Value;
                    }
                    _contractHistoryService.UpdateContractHistory(existingHistory);

                    return Ok(new
                    {
                        message = "Cập nhật lịch sử chuyển nhượng thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Lịch sử chuyển nhượng không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
