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
    public class ContractPaymentDetailRepo : IContractPaymentDetailRepo
    {
        ContractPaymentDetailDAO dao = new ContractPaymentDetailDAO();

        public void AddNewContractPaymentDetail(ContractPaymentDetail detail) => dao.AddNewContractPaymentDetail(detail);

        public void DeleteContractPaymentDetailByID(Guid id) => dao.DeleteContractPaymentDetailByID(id);

        public List<ContractPaymentDetail> GetAllContractPaymentDetail() => dao.GetAllContractPaymentDetail();

        public List<ContractPaymentDetail> GetContractPaymentDetailByContractID(Guid contractId) => dao.GetContractPaymentDetailByContractID(contractId);

        public ContractPaymentDetail GetContractPaymentDetailByID(Guid id) => dao.GetContractPaymentDetailByID(id);

        public void UpdateContractPaymentDetail(ContractPaymentDetail detail) => dao.UpdateContractPaymentDetail(detail);

    }
}
