using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
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
        private readonly IBookingServices _bookServices;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IMapper _mapper;

        public ContractController(IContractServices contractServices, IBookingServices bookServices, BlobServiceClient blobServiceClient, IMapper mapper)
        {
            _contractServices = contractServices;
            _bookServices = bookServices;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Contract")]
        public IActionResult GetAllContract()
        {
            try
            {
                if (_contractServices.GetAllContract() == null)
                {
                    return NotFound();
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

            return NotFound();

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Contract")]
        public IActionResult AddNewContract([FromForm] ContractRequestDTO contract)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("contractfile");
                string? blobUrl = null;
                if (contract.ContractFile != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{contract.ContractFile.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(contract.ContractFile.OpenReadStream());
                    var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/contractfile";
                    blobUrl = $"{storageAccountUrl}/{blobName}";
                }

                string nextContractCode = GenerateNextContractCode();

                var newContract = new ContractCreateDTO
                {
                    ContractID = Guid.NewGuid(),
                    ContractCode = nextContractCode,
                    ContractName = contract.ContractName,
                    ContractType = contract.ContractType,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    DateSigned = null,
                    ExpiredTime = contract.ExpiredTime,
                    TotalPrice = contract.TotalPrice,
                    Description = contract.Description,
                    ContractFile = contract.ContractFile,
                    Status = ContractStatus.NotSigned.ToString(),
                    DocumentID = contract.DocumentID,
                    BookingID = contract.BookingID,
                    PaymentProcessID = contract.PaymentProcessID,

                };

                var _contract = _mapper.Map<Contract>(newContract);
                _contract.ContractFile = blobUrl;
                _contractServices.AddNewContract(_contract);

                return Ok("Create Contract Successfully");
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
                var containerInstance = _blobServiceClient.GetBlobContainerClient("contractfile");
                string? blobUrl = null;
                if (contract.ContractFile != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{contract.ContractFile.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(contract.ContractFile.OpenReadStream());
                    var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/contractfile";
                    blobUrl = $"{storageAccountUrl}/{blobName}";
                }

                var existingContract = _contractServices.GetContractByID(id);
                if (existingContract != null)
                {
                    if (!string.IsNullOrEmpty(contract.ContractCode))
                    {
                        existingContract.ContractCode = contract.ContractCode;
                    }
                    if (!string.IsNullOrEmpty(contract.ContractName))
                    {
                        existingContract.ContractName = contract.ContractName;
                    }
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
                    if (!string.IsNullOrEmpty(contract.Status))
                    {
                        existingContract.Status = contract.Status;
                    }
                    //if (contract.DocumentID.HasValue)
                    //{
                    //    existingContract.DocumentTemplateID = contract.DocumentID.Value;
                    //}
                    if (contract.PaymentProcessID.HasValue)
                    {
                        existingContract.PaymentProcessID = contract.PaymentProcessID.Value;
                    }
                    if (blobUrl != null)
                    {
                        existingContract.ContractFile = blobUrl;
                    }

                    existingContract.UpdatedTime = DateTime.Now;
                    _contractServices.UpdateContract(existingContract);

                    return Ok("Update Contract Successfully");

                }

                return NotFound("Contract not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/signed")]
        [SwaggerOperation(Summary = "Customer Signed Contract by ID")]
        public IActionResult CustomerSignedContract(Guid id)
        {
            try
            {
                var contract = _contractServices.GetContractByID(id);
                var book = _bookServices.GetBookingById(contract.BookingID);
                if (contract != null && book != null)
                {
                    contract.DateSigned = DateTime.Now;
                    contract.Status = ContractStatus.Signed.ToString();

                    book.Status = BookingStatus.ContractSigned.ToString();

                    _contractServices.UpdateContract(contract);
                    _bookServices.UpdateBooking(book);
                    return Ok("Signed Contract Successfully");

                }

                return NotFound("Contract not found.");

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
                return NotFound();
            }

            _contractServices.ChangeStatusContract(contract);


            return Ok("Delete Successfully");
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
