using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IContractHistoryRepo
    {
        void DeleteContractHistory(Guid id);


        List<ContractHistory> GetContractHistorys();
        void AddNewContractHistory(ContractHistory p);


        ContractHistory GetContractHistoryById(Guid id);
        List<ContractHistory> GetContractHistoryByContractID(Guid id);
        void UpdateContractHistory(ContractHistory p);

    }
}
