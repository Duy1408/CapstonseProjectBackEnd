using AutoMapper;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Stripe;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.Contracts;


namespace RealEstateProjectSale.Controllers.ContractHistoryController
{
    [Route("api/contracthistorys")]
    [ApiController]
    public class ContractHistoryController : ControllerBase
    {
        private readonly IContractHistoryServices _contracthistory;
        private readonly IMapper _mapper;
        private readonly IFileUploadToBlobService _file;
        public ContractHistoryController(IContractHistoryServices contracthistory, IMapper mapper, IFileUploadToBlobService file)
        {
            _contracthistory = contracthistory;
            _mapper = mapper;
            _file = file;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all ContractHistory")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách ContractHistory.", typeof(List<ContractHistoryVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Contract History không tồn tại.")]
        public IActionResult GetAllContractHistory()
        {
            try
            {
                if (_contracthistory.GetContractHistorys() == null)
                {
                    return NotFound(new
                    {
                        message = "Lịch sử chuyển nhượng không tồn tại."
                    });
                }
                var contracthistorys = _contracthistory.GetContractHistorys();
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
            var contracthistory = _contracthistory.GetContractHistoryById(id);

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
            var contracthistory = _contracthistory.GetContractHistoryByContractID(contractId);

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

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete contract history")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xóa contract history thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "contract history không tồn tại.")]
        public IActionResult DeleteContractHistory(Guid id)
        {

            var contracthistory = _contracthistory.GetContractHistoryById(id);
            if (contracthistory == null)
            {
                return NotFound(new
                {
                    message = "Lịch sử chuyển nhượng không tồn tại."
                });
            }

            _contracthistory.DeleteContractHistory(id);

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
                var imageUrls = _file.UploadSingleImage(history.AttachFile, "attachfile");

                var newContractHistory = new ContractHistoryCreateDTO
                {
                    ContractHistoryID = Guid.NewGuid(),
                    NotarizedContractCode = history.NotarizedContractCode,
                    Note = history.Note,
                    CustomerID = history.CustomerID,
                    ContractID = history.ContractID,
                    AttachFile = history.AttachFile,
                };

                var contracthistory = _mapper.Map<ContractHistory>(newContractHistory);
                contracthistory.AttachFile = imageUrls;
                _contracthistory.AddNewContractHistory(contracthistory);

                return Ok(new
                {
                    message = "Tạo lịch sử chuyển nhượng thành công."
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
                if (history.AttachFile != null)
                {
                    blobUrl = _file.UploadSingleImage(history.AttachFile, "attachfile");
                }

                var existingHistory = _contracthistory.GetContractHistoryById(id);
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
                    if (history.ContractID.HasValue)
                    {
                        existingHistory.ContractID = history.ContractID.Value;
                    }
                    _contracthistory.UpdateContractHistory(existingHistory);

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
