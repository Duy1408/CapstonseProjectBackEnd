using Humanizer;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using Stripe;
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
        private readonly IOpenForSaleDetailServices _openDetailService;
        private readonly IPromotionDetailServices _promotionDetailService;

        public ContractServices(IContractRepo contractRepo, IDocumentTemplateService documentService,
            ICustomerServices customerService, IProjectServices projectService, IBookingServices bookingService,
            IProjectCategoryDetailServices detailService, IPropertyServices propertyService, IUnitTypeServices unitTypeService,
            IPropertyTypeServices propertyTypeService, IPaymentProcessDetailServices pmtDetailService, IOpenForSaleDetailServices openDetailService,
            IPromotionDetailServices promotionDetailService)
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
            _openDetailService = openDetailService;
            _promotionDetailService = promotionDetailService;
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

        public string GenerateDocumentPriceSheet(Guid contractId)
        {
            var contract = _contractRepo.GetContractByID(contractId);
            var documentTemplate = _documentService.GetDocumentById(contract.DocumentTemplateID);
            if (documentTemplate == null)
            {
                throw new Exception("Document template not found");
            }

            var promotionDetailId = contract.PromotionDetailID.GetValueOrDefault(Guid.Empty);
            if (promotionDetailId == Guid.Empty)
            {
                throw new ArgumentException("Booking không có căn hộ.");
            }
            var promotionDetail = _promotionDetailService.GetPromotionDetailByID(promotionDetailId);
            var booking = _bookingService.GetBookingById(contract.BookingID);
            var property = _propertyService.GetPropertyById(booking.PropertyID!.Value);
            var propertyId = booking.PropertyID.GetValueOrDefault(Guid.Empty);
            if (propertyId == Guid.Empty)
            {
                throw new ArgumentException("Booking không có căn hộ.");
            }
            var openDetail = _openDetailService.GetDetailByPropertyIdOpenId(propertyId, booking.OpeningForSaleID);
            var unitType = _unitTypeService.GetUnitTypeByID(property.UnitTypeID!.Value);

            //Tính tiền
            var pricePromotion = openDetail.Price - promotionDetail.Amount;
            var vat = pricePromotion * 0.1;
            var priceIncludingVAT = pricePromotion + vat;
            var maintenanceCost = pricePromotion * 0.02;
            var totalPrice = priceIncludingVAT + maintenanceCost;
            string updatedTime = contract.UpdatedTime.HasValue
                                        ? contract.UpdatedTime.Value.ToString("yyyy/MM/dd HH:mm")
                                        : "";

            property.PriceSold = totalPrice;
            _propertyService.UpdateProperty(property);
            var paymentProcessTableHtml = GeneratePaymentProcessTable(contract.PaymentProcessID, totalPrice);

            var htmlContent = documentTemplate.DocumentFile;
            htmlContent = htmlContent.Replace("{PropertyCode}", property.PropertyCode)
                                     .Replace("{UpdatedTimeContract}", updatedTime)
                                     .Replace("{NetFloorArea}", unitType.NetFloorArea.ToString())
                                     .Replace("{GrossFloorArea}", unitType.GrossFloorArea.ToString())
                                     .Replace("{PriceOpen}", openDetail.Price.ToString())
                                     .Replace("{AmountPromotion}", promotionDetail.Amount.ToString())
                                     .Replace("{VAT}", vat.ToString())
                                     .Replace("{PriceIncludingVAT}", priceIncludingVAT.ToString())
                                     .Replace("{MaintenanceCost}", maintenanceCost.ToString())
                                     .Replace("{TotalPrice}", totalPrice.ToString())
                                     .Replace("{PaymentProcessTable}", paymentProcessTableHtml);

            return htmlContent;

        }

        public string GeneratePaymentProcessTable(Guid? paymentprocessId, double? totalPrice)
        {
            var pmtId = paymentprocessId.GetValueOrDefault(Guid.Empty);
            if (pmtId == Guid.Empty)
            {
                throw new ArgumentException("Booking không có căn hộ.");
            }
            var paymentDetails = _pmtDetailService.GetPaymentProcessDetailByPaymentProcessID(pmtId)
                                          .OrderBy(detail => detail.PaymentStage)
                                          .ToList();

            double? totalAmount = totalPrice;
            double? firstAmount = paymentDetails.FirstOrDefault()?.Amount;
            var tableHtml = new StringBuilder();

            for (int i = 0; i < paymentDetails.Count; i++)
            {
                var detail = paymentDetails[i];
                string paymentStage = detail.PaymentStage > 0 ? $"Đợt {detail.PaymentStage}" : "Đợt 1: Ký TTĐC";
                string period = detail.Period.HasValue ? detail.Period.Value.ToString("dd-MM-yyyy") : "";
                string percentage = detail.Percentage.HasValue ? $"{(detail.Percentage.Value * 100):N0}%" : "0%";

                double? amountValue;

                // Kiểm tra nếu là dòng cuối cùng
                if (i == paymentDetails.Count - 1)
                {
                    // Nếu là dòng cuối cùng, tính amount theo công thức điều chỉnh với dòng đầu tiên
                    amountValue = (totalAmount * (detail.Percentage ?? 0)) - (firstAmount ?? 0);
                }
                else
                {
                    // Tính amount bình thường nếu không phải là dòng cuối
                    amountValue = detail.Amount ?? (totalAmount * (detail.Percentage ?? 0));
                }

                string amount = $"{amountValue:N0} VND";

                tableHtml.Append($@"
                <tr>
                    <td>{paymentStage}: {detail.Description}</td>
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
