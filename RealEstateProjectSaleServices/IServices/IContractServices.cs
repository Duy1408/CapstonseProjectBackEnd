using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IContractServices
    {
        List<Contract> GetAllContract();
        void AddNewContract(Contract contract);
        Contract GetContractByID(Guid id);
        List<Contract> GetContractByCustomerID(Guid id);
        void UpdateContract(Contract contract);
        bool ChangeStatusContract(Contract contract);
        Contract GetLastContract();
        string GenerateDocumentDeposit(Guid bookingId);
    }
}
