using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class ContractRepo : IContractRepo
    {
        ContractDAO dao = new ContractDAO();

        public void AddNewContract(Contract contract) => dao.AddNewContract(contract);

        public bool ChangeStatusContract(Contract contract) => dao.ChangeStatusContract(contract);

        public List<Contract> GetAllContract() => dao.GetAllContract();

        public Contract GetContractByID(Guid id) => dao.GetContractByID(id);

        public Contract GetLastContract() => dao.GetLastContract();

        public void UpdateContract(Contract contract) => dao.UpdateContract(contract);

    }
}
