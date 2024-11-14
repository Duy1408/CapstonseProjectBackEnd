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
    public class ContractPaymentDetailServices : IContractPaymentDetailServices
    {
        private readonly IContractPaymentDetailRepo _detail;
        public ContractPaymentDetailServices(IContractPaymentDetailRepo detail)
        {
            _detail = detail;
        }

        public void AddNewContractPaymentDetail(ContractPaymentDetail detail) => _detail.AddNewContractPaymentDetail(detail);

        public void DeleteContractPaymentDetailByID(Guid id) => _detail.DeleteContractPaymentDetailByID(id);

        public List<ContractPaymentDetail> GetAllContractPaymentDetail() => _detail.GetAllContractPaymentDetail();

        public List<ContractPaymentDetail> GetContractPaymentDetailByContractID(Guid contractId) => _detail.GetContractPaymentDetailByContractID(contractId);

        public ContractPaymentDetail GetContractPaymentDetailByID(Guid id) => _detail.GetContractPaymentDetailByID(id);

        public void UpdateContractPaymentDetail(ContractPaymentDetail detail) => _detail.UpdateContractPaymentDetail(detail);

    }
}
