using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.ContractController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractServices _contractServices;
        private readonly IMapper _mapper;

        public ContractController(IContractServices contractServices, IMapper mapper)
        {
            _contractServices = contractServices;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllContract")]
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

        [HttpGet("GetContractByID/{id}")]
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
        [Route("AddNewComment")]
        public IActionResult AddNewContract([FromForm] ContractRequestDTO contract)
        {
            try
            {
                // Chuyển đổi IFormFile sang byte[]
                byte[]? contractFileBytes = null;
                if (contract.ContractFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        contract.ContractFile.CopyTo(ms);
                        contractFileBytes = ms.ToArray();
                    }
                }

                var newContract = new ContractCreateDTO
                {
                    ContractID = Guid.NewGuid(),
                    ContractName = contract.ContractName,
                    ContractType = contract.ContractType,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    DateSigned = null,
                    ExpiredTime = contract.ExpiredTime,
                    TotalPrice = contract.TotalPrice,
                    Description = contract.Description,
                    ContractFile = contractFileBytes,
                    Status = contract.Status,
                    BookingID = contract.BookingID,
                    PaymentProcessID = contract.PaymentProcessID,

                };

                var _contract = _mapper.Map<Contract>(newContract);
                _contractServices.AddNewContract(_contract);

                return Ok("Create Contract Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateContract/{id}")]
        public IActionResult UpdateContract([FromForm] ContractUpdateDTO contract, Guid id)
        {
            try
            {
                // Chuyển đổi IFormFile sang byte[]
                byte[]? contractFileBytes = null;
                if (contract.ContractFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        contract.ContractFile.CopyTo(ms);
                        contractFileBytes = ms.ToArray();
                    }
                }

                var existingContract = _contractServices.GetContractByID(id);
                if (existingContract != null)
                {
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
                    if (contractFileBytes != null)
                    {
                        existingContract.ContractFile = contractFileBytes;
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

        [HttpPut("UpdateContract/SignedContract/{id}")]
        public IActionResult CustomerSignedContract(Guid id)
        {
            try
            {
                var contract = _contractServices.GetContractByID(id);
                if (contract != null)
                {
                    contract.DateSigned = DateTime.Now;
                    contract.Status = "Khách hàng đã ký hợp đồng mua bán";

                    _contractServices.UpdateContract(contract);
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


    }
}
