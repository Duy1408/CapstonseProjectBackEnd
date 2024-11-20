using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Org.BouncyCastle.Crypto;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Contract = RealEstateProjectSaleBusinessObject.BusinessObject.Contract;

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
        private readonly IPaymentProcessServices _pmtService;
        private readonly IPaymentProcessDetailServices _pmtDetailService;
        private readonly IOpenForSaleDetailServices _openDetailService;
        private readonly IPromotionDetailServices _promotionDetailService;
        private readonly IContractPaymentDetailServices _contractDetailService;
        private readonly IMapper _mapper;

        public ContractServices(IContractRepo contractRepo, IDocumentTemplateService documentService,
            ICustomerServices customerService, IProjectServices projectService, IBookingServices bookingService,
            IProjectCategoryDetailServices detailService, IPropertyServices propertyService, IUnitTypeServices unitTypeService,
            IPropertyTypeServices propertyTypeService, IPaymentProcessDetailServices pmtDetailService, IOpenForSaleDetailServices openDetailService,
            IPromotionDetailServices promotionDetailService, IContractPaymentDetailServices contractDetailService, IMapper mapper,
            IPaymentProcessServices pmtService)
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
            _contractDetailService = contractDetailService;
            _pmtService = pmtService;
            _mapper = mapper;
        }

        public string GenerateDocumentSale(Guid contractId)
        {
            var contract = _contractRepo.GetContractByID(contractId);
            var documentTemplate = _documentService.GetDocumentById(contract.DocumentTemplateID);
            if (documentTemplate == null)
            {
                throw new Exception("Document template not found");
            }
            var paymentProcess = _pmtService.GetPaymentProcessById(contract.PaymentProcessID!.Value);
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
                                     .Replace("{Location}", project.Location)
                                     .Replace("{TotalPrice}", property.PriceSold.ToString())
                                     .Replace("{MoneyText}", property.PriceSold.HasValue
                                        ? char.ToUpper(((long)Math.Round(property.PriceSold.Value)).ToWords(new CultureInfo("vi"))[0]) +
                                        ((long)Math.Round(property.PriceSold.Value)).ToWords(new CultureInfo("vi")).Substring(1) +
                                        " đồng chẵn." : "N/A")
                                     .Replace("{PaymentProcessName}", paymentProcess.PaymentProcessName);

            return htmlContent;
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

        public string GenerateDocumentTransfer(Guid contractId, Guid customerTwoId)
        {
            var contract = _contractRepo.GetContractByID(contractId);
            var documentTemplate = _documentService.GetDocumentById(contract.DocumentTemplateID);
            if (documentTemplate == null)
            {
                throw new Exception("Document template not found");
            }
            var customerOne = _customerService.GetCustomerByID(contract.CustomerID);
            var customerTwo = _customerService.GetCustomerByID(customerTwoId);
            var booking = _bookingService.GetBookingById(contract.BookingID);
            var categoryDetail = _detailService.GetProjectCategoryDetailByID(booking.ProjectCategoryDetailID);
            var project = _projectService.GetProjectById(categoryDetail.ProjectID);
            var property = _propertyService.GetPropertyById(booking.PropertyID!.Value);
            var unitType = _unitTypeService.GetUnitTypeByID(property.UnitTypeID!.Value);
            var propertyType = _propertyTypeService.GetPropertyTypeByID(unitType.PropertyTypeID!.Value);

            var htmlContent = documentTemplate.DocumentFile;
            htmlContent = htmlContent.Replace("{FullNameOne}", customerOne.FullName)
                                     .Replace("{FullNameTwo}", customerTwo.FullName)
                                     .Replace("{DateOfBirthOne}", customerOne.DateOfBirth.ToString("yyyy/MM/dd"))
                                     .Replace("{DateOfBirthTwo}", customerTwo.DateOfBirth.ToString("yyyy/MM/dd"))
                                     .Replace("{IdentityCardNumberOne}", customerOne.IdentityCardNumber)
                                     .Replace("{IdentityCardNumberTwo}", customerTwo.IdentityCardNumber)
                                     .Replace("{AddressOne}", customerOne.Address)
                                     .Replace("{AddressTwo}", customerTwo.Address)
                                     .Replace("{PhoneNumberOne}", "0" + customerOne.PhoneNumber)
                                     .Replace("{PhoneNumberTwo}", "0" + customerTwo.PhoneNumber)
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
            var paymentProcessTableHtml = GeneratePaymentProcessTable(contractId, contract.PaymentProcessID, totalPrice);

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

        public string GeneratePaymentProcessTable(Guid contractId, Guid? paymentprocessId, double? totalPrice)
        {
            var contract = _contractRepo.GetContractByID(contractId);
            var booking = _bookingService.GetBookingById(contract.BookingID);

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

            //Ngày đợt 1
            DateTime periodFirst = DateTime.Now;

            for (int i = 0; i < paymentDetails.Count; i++)
            {
                var detail = paymentDetails[i];
                string paymentStage = detail.PaymentStage > 0 ? $"Đợt {detail.PaymentStage}" : "Đợt 1: Ký TTĐC";

                //Tính Period ở ContractPaymentDetail
                string period;
                if (detail.PaymentStage == 1)
                {
                    period = periodFirst.ToString("dd-MM-yyyy");
                }
                else if (detail.DurationDate.HasValue)
                {
                    // Cộng ngày từ đợt 1
                    DateTime calculatedDate = periodFirst.AddDays(detail.DurationDate.Value);
                    period = calculatedDate.ToString("dd-MM-yyyy");
                }
                else
                {
                    period = "";
                }

                string percentage = detail.Percentage.HasValue ? $"{(detail.Percentage.Value * 100):N0}%" : "0%";

                double? amountValue;

                // Kiểm tra nếu là dòng cuối cùng
                if (i == paymentDetails.Count - 1)
                {
                    // Nếu là dòng cuối cùng, tính amount theo công thức trừ tiền đợt 1 và giữ chỗ booking
                    amountValue = (totalAmount * (detail.Percentage ?? 0)) - (firstAmount ?? 0) - (booking.DepositedPrice ?? 0);
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

        public void CreateContractPaymentDetail(Guid contractId)
        {
            var contract = _contractRepo.GetContractByID(contractId);
            var pmtId = contract.PaymentProcessID.GetValueOrDefault(Guid.Empty);
            if (pmtId == Guid.Empty)
            {
                throw new ArgumentException("Booking không có căn hộ.");
            }
            var paymentDetails = _pmtDetailService.GetPaymentProcessDetailByPaymentProcessID(pmtId)
                                  .OrderBy(detail => detail.PaymentStage)
                                  .ToList();
            var booking = _bookingService.GetBookingById(contract.BookingID);
            var propertyId = booking.PropertyID.GetValueOrDefault(Guid.Empty);
            if (pmtId == Guid.Empty)
            {
                throw new ArgumentException("Booking không có căn hộ.");
            }
            var property = _propertyService.GetPropertyById(propertyId);

            var categoryDetail = _detailService.GetProjectCategoryDetailByID(booking.ProjectCategoryDetailID);
            var project = _projectService.GetProjectById(categoryDetail.ProjectID);

            double? totalAmount = property.PriceSold;
            double? firstAmount = paymentDetails.FirstOrDefault()?.Amount;

            //Ngày đợt 1
            DateTime periodFirst = DateTime.Now;

            for (int i = 0; i < paymentDetails.Count; i++)
            {
                var detail = paymentDetails[i];
                double? amountValue;

                // Kiểm tra nếu là dòng cuối cùng
                if (i == paymentDetails.Count - 1)
                {
                    // Nếu là dòng cuối cùng, tính amount theo công thức trừ tiền đợt 1 và giữ chỗ booking
                    amountValue = (totalAmount * (detail.Percentage ?? 0)) - (firstAmount ?? 0) - (booking.DepositedPrice ?? 0);
                }
                else
                {
                    // Tính amount bình thường nếu không phải là dòng cuối
                    amountValue = detail.Amount ?? (totalAmount * (detail.Percentage ?? 0));
                }

                amountValue = amountValue.HasValue ? Math.Round(amountValue.Value) : (double?)null;

                // Tính toán Period
                DateTime? period = null;
                if (detail.PaymentStage == 1)
                {
                    period = periodFirst; // Ngày hiện tại cho đợt 1
                }
                else if (detail.DurationDate.HasValue)
                {
                    // Cộng ngày từ đợt 1
                    period = periodFirst.AddDays(detail.DurationDate.Value);
                }

                // Tạo đối tượng chi tiết thanh toán và lưu vào cơ sở dữ liệu
                var contractDetail = new ContractPaymentDetailCreateDTO
                {
                    ContractPaymentDetailID = Guid.NewGuid(),
                    PaymentRate = detail.PaymentStage,
                    Description = detail.Description,
                    Period = period.HasValue ? period.Value : throw new InvalidOperationException("Không tính được Period"),
                    PaidValue = amountValue,
                    PaidValueLate = null,
                    RemittanceOrder = null,
                    Status = false,
                    ContractID = contractId,
                    PaymentPolicyID = project.PaymentPolicyID
                };

                var _detail = _mapper.Map<ContractPaymentDetail>(contractDetail);

                _contractDetailService.AddNewContractPaymentDetail(_detail);
            }

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
