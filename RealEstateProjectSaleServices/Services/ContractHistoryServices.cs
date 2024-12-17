using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class ContractHistoryServices : IContractHistoryServices
    {
        private IContractHistoryRepo _repo;
        public ContractHistoryServices(IContractHistoryRepo repo)
        {
            _repo = repo;
        }
        public void AddNewContractHistory(ContractHistory p)
        {
            _repo.AddNewContractHistory(p);
        }

        public ContractHistory CheckNotarizedContractCode(string notarizedCode)
        {
            return _repo.CheckNotarizedContractCode(notarizedCode);
        }

        public void DeleteContractHistory(Guid id)
        {
            _repo.DeleteContractHistory(id);
        }

        public List<ContractHistory> GetContractHistoryByContractID(Guid id)
        {
            return _repo.GetContractHistoryByContractID(id);
        }

        public ContractHistory GetContractHistoryById(Guid id)
        {
            return _repo.GetContractHistoryById(id);
        }

        public List<ContractHistory> GetContractHistorys()
        {
            return _repo.GetContractHistorys();
        }

        public void UpdateContractHistory(ContractHistory p)
        {
            _repo.UpdateContractHistory(p);
        }
    }
}
