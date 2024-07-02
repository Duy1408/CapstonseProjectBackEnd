using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
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
        public IActionResult AddNewContract(ContractCreateDTO contract)
        {
            try
            {
                var _contract = _mapper.Map<Contract>(contract);
                _contractServices.AddNewContract(_contract);

                return Ok("Create Contract Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContract(ContractUpdateDTO contract, Guid id)
        {
            try
            {
                var existingContract = _contractServices.GetContractByID(id);
                if (existingContract != null)
                {
                    contract.ContractID = existingContract.ContractID;
                    contract.BookingID = existingContract.BookingID;

                    var _contract = _mapper.Map<Contract>(contract);
                    _contractServices.UpdateContract(_contract);

                    return Ok("Update Successfully");

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
