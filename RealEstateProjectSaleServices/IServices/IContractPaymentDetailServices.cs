using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IContractPaymentDetailServices
    {
        List<ContractPaymentDetail> GetAllContractPaymentDetail();
        void AddNewContractPaymentDetail(ContractPaymentDetail detail);
        ContractPaymentDetail GetContractPaymentDetailByID(Guid id);
        void UpdateContractPaymentDetail(ContractPaymentDetail detail);
        void DeleteContractPaymentDetailByID(Guid id);
        List<ContractPaymentDetail> GetContractPaymentDetailByContractID(Guid contractId);
    }
}
