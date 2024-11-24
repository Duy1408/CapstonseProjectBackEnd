using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.ViewModels;
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
        private readonly IPaymentPolicyService _policyService;
        public ContractPaymentDetailServices(IContractPaymentDetailRepo detail, IPaymentPolicyService policyService)
        {
            _detail = detail;
            _policyService = policyService;
        }

        public void AddNewContractPaymentDetail(ContractPaymentDetail detail) => _detail.AddNewContractPaymentDetail(detail);

        public void DeleteContractPaymentDetailByID(Guid id) => _detail.DeleteContractPaymentDetailByID(id);

        public List<ContractPaymentDetail> GetAllContractPaymentDetail() => _detail.GetAllContractPaymentDetail();

        public List<ContractPaymentDetail> GetContractPaymentDetailByContractID(Guid contractId) => _detail.GetContractPaymentDetailByContractID(contractId);

        public ContractPaymentDetail GetContractPaymentDetailByID(Guid id) => _detail.GetContractPaymentDetailByID(id);

        public void UpdateContractPaymentDetail(ContractPaymentDetail detail) => _detail.UpdateContractPaymentDetail(detail);

        public double? CalculateLatePaymentInterest(Guid contractDetailId)
        {
            var contractDetail = _detail.GetContractPaymentDetailByID(contractDetailId);
            var policyId = contractDetail.PaymentPolicyID.GetValueOrDefault(Guid.Empty);
            if (policyId == Guid.Empty)
            {
                return null;
            }
            var paymentPolicy = _policyService.GetPaymentPolicyByID(policyId);

            if (DateTime.Now > contractDetail.Period && contractDetail.Status == false)
            {
                var actualLateDays = (DateTime.Now - contractDetail.Period.Value).TotalDays;

                if (actualLateDays >= paymentPolicy.LateDate)
                {
                    contractDetail.PaidValueLate = contractDetail.PaidValue
                        * paymentPolicy.PercentLate
                        * actualLateDays;

                    contractDetail.PaidValueLate = Math.Round(contractDetail.PaidValueLate.Value);

                    _detail.UpdateContractPaymentDetail(contractDetail);

                    return contractDetail.PaidValueLate;
                }
            }

            return contractDetail.PaidValueLate;

        }

    }
}
