using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class ContractServices : IContractServices
    {
        private readonly IContractRepo _contractRepo;
        public ContractServices(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public void AddNewContract(Contract contract) => _contractRepo.AddNewContract(contract);

        public bool ChangeStatusContract(Contract contract) => _contractRepo.ChangeStatusContract(contract);

        public List<Contract> GetAllContract() => _contractRepo.GetAllContract();

        public Contract GetContractByID(Guid id) => _contractRepo.GetContractByID(id);

        public Contract GetLastContract() => _contractRepo.GetLastContract();

        public void UpdateContract(Contract contract) => _contractRepo.UpdateContract(contract);

    }
}
