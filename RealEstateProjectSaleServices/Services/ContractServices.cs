using Humanizer;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class ContractServices : IContractServices
    {
        private readonly IContractRepo _contractRepo;
        private readonly IDocumentTemplateService _documentService;
        private readonly ICustomerServices _customerService;
        private readonly IProjectServices _projectService;
        private readonly IBookingServices _bookingService;
        private readonly IProjectCategoryDetailServices _detailService;
        private readonly IPropertyServices _propertyService;
        private readonly IUnitTypeServices _unitTypeService;
        private readonly IPropertyTypeServices _propertyTypeService;
        private readonly IPaymentProcessDetailServices _pmtDetailService;

        public ContractServices(IContractRepo contractRepo, IDocumentTemplateService documentService,
            ICustomerServices customerService, IProjectServices projectService, IBookingServices bookingService,
            IProjectCategoryDetailServices detailService, IPropertyServices propertyService, IUnitTypeServices unitTypeService,
            IPropertyTypeServices propertyTypeService, IPaymentProcessDetailServices pmtDetailService)
        {
            _contractRepo = contractRepo;
            _documentService = documentService;
            _customerService = customerService;
            _projectService = projectService;
            _bookingService = bookingService;
            _detailService = detailService;
            _propertyService = propertyService;
            _unitTypeService = unitTypeService;
            _propertyTypeService = propertyTypeService;
            _pmtDetailService = pmtDetailService;
        }

        public string GenerateDocumentDeposit(Guid contractId)
        {
            var contract = _contractRepo.GetContractByID(contractId);
            var documentTemplate = _documentService.GetDocumentById(contract.DocumentTemplateID);
            if (documentTemplate == null)
            {
                throw new Exception("Document template not found");
            }
            var customer = _customerService.GetCustomerByID(contract.CustomerID);
            var booking = _bookingService.GetBookingById(contract.BookingID);
            var categoryDetail = _detailService.GetProjectCategoryDetailByID(booking.ProjectCategoryDetailID);
            var project = _projectService.GetProjectById(categoryDetail.ProjectID);
            var property = _propertyService.GetPropertyById(booking.PropertyID!.Value);
            var unitType = _unitTypeService.GetUnitTypeByID(property.UnitTypeID!.Value);
            var propertyType = _propertyTypeService.GetPropertyTypeByID(unitType.PropertyTypeID!.Value);

            var htmlContent = documentTemplate.DocumentFile;
            htmlContent = htmlContent.Replace("{FullName}", customer.FullName)
                                     .Replace("{DateOfBirth}", customer.DateOfBirth.ToString("yyyy/MM/dd"))
                                     .Replace("{IdentityCardNumber}", customer.IdentityCardNumber)
                                     .Replace("{Address}", customer.Address)
                                     .Replace("{PhoneNumber}", "0" + customer.PhoneNumber)
                                     .Replace("{ProjectName}", project.ProjectName)
                                     .Replace("{PropertyCode}", property.PropertyCode)
                                     .Replace("{PropertyType}", propertyType.PropertyTypeName)
                                     .Replace("{NetFloorArea}", unitType.NetFloorArea.ToString())
                                     .Replace("{Location}", project.Location);

            return htmlContent;
        }

        public string GeneratePaymentProcessTable(Guid contractId, Guid paymentprocessId)
        {
            var paymentDetails = _pmtDetailService.GetPaymentProcessDetailByPaymentProcessID(paymentprocessId);

            double totalAmount = 0;
            var tableHtml = new StringBuilder();

            foreach (var detail in paymentDetails)
            {
                string paymentStage = detail.PaymentStage > 1 ? $"Đợt {detail.PaymentStage}" : "TTĐC - Lần 1";
                string period = detail.Period.HasValue ? detail.Period.Value.ToString("dd-MM-yyyy") : "";
                string percentage = detail.Percentage.HasValue ? $"{detail.Percentage.Value * 100}%" : "0%";
                string amount = detail.Amount.HasValue ? $"{detail.Amount.Value:N0} VND" : "0 VND";

                totalAmount += detail.Amount ?? 0;

                tableHtml.Append($@"
                <tr>
                    <td>{paymentStage} {detail.Description}</td>
                    <td>{period}</td>
                    <td style='text-align:center'>{percentage}</td>
                    <td style='text-align:right'>{amount}</td>
                </tr>");
            }

            tableHtml.Append($@"
            <tr>
                <td colspan='3' style='text-align:right'><strong>Tổng cộng</strong></td>
                <td style='background-color:#d4a014; text-align:right'><strong>{totalAmount:N0} VND</strong></td>
            </tr>");

            return tableHtml.ToString();

        }

        public void AddNewContract(Contract contract) => _contractRepo.AddNewContract(contract);

        public bool ChangeStatusContract(Contract contract) => _contractRepo.ChangeStatusContract(contract);

        public List<Contract> GetAllContract() => _contractRepo.GetAllContract();

        public List<Contract> GetContractByCustomerID(Guid id) => _contractRepo.GetContractByCustomerID(id);

        public Contract GetContractByID(Guid id) => _contractRepo.GetContractByID(id);

        public Contract GetLastContract() => _contractRepo.GetLastContract();

        public void UpdateContract(Contract contract) => _contractRepo.UpdateContract(contract);

    }
}
