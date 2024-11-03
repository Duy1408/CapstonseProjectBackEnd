using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using System.Globalization;

namespace RealEstateProjectSaleServices.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _book;
        private readonly IDocumentTemplateService _documentService;
        private readonly ICustomerServices _customerService;
        private readonly IProjectServices _projectService;
        private readonly IPropertyCategoryServices _categoryService;
        private readonly IProjectCategoryDetailServices _detailService;
        public BookingServices(IBookingRepo book, IDocumentTemplateService documentService,
            ICustomerServices customerService, IProjectServices projectService, IPropertyCategoryServices categoryService,
            IProjectCategoryDetailServices detailService)
        {
            _book = book;
            _documentService = documentService;
            _customerService = customerService;
            _projectService = projectService;
            _categoryService = categoryService;
            _detailService = detailService;
        }

        public string GenerateDocumentContent(Guid bookingId)
        {
            var booking = _book.GetBookingById(bookingId);
            var documentTemplate = _documentService.GetDocumentById(booking.DocumentTemplateID);
            if (documentTemplate == null)
            {
                throw new Exception("Document template not found");
            }
            var customer = _customerService.GetCustomerByID(booking.CustomerID);
            var categoryDetail = _detailService.GetProjectCategoryDetailByID(booking.ProjectCategoryDetailID);
            var project = _projectService.GetProjectById(categoryDetail.ProjectID);
            var propertyCategory = _categoryService.GetPropertyCategoryByID(categoryDetail.PropertyCategoryID);

            var htmlContent = documentTemplate.DocumentFile;

            htmlContent = htmlContent.Replace("{Logo}", "<img src='https://realestatesystem.blob.core.windows.net/realestate/Logo.png' alt='Logo' style='width:100px; height:auto;'>")
                             .Replace("{ProjectName}", project.ProjectName)
                             .Replace("{Location}", project.Location)
                             .Replace("{PropertyCategoryName}", propertyCategory.PropertyCategoryName)
                             .Replace("{FullName}", customer.FullName)
                             .Replace("{IdentityCardNumber}", customer.IdentityCardNumber)
                             .Replace("{Address}", customer.Address)
                             .Replace("{PhoneNumber}", "0" + customer.PhoneNumber)
                             .Replace("{Reason}", "Nộp tiền giữ chỗ tham gia sự kiện mở bán dự án " + project.ProjectName)
                             .Replace("{DepositedTimed}", booking.DepositedTimed.ToString())
                             .Replace("{CreatedTime}", booking.CreatedTime.ToString())
                             .Replace("{DepositedPrice}", booking.DepositedPrice.ToString() + " VND")
                             .Replace("{MoneyText}", booking.DepositedPrice.HasValue
                                    ? char.ToUpper(((int)Math.Round(booking.DepositedPrice.Value)).ToWords(new CultureInfo("vi"))[0]) +
                                    ((int)Math.Round(booking.DepositedPrice.Value)).ToWords(new CultureInfo("vi")).Substring(1) +
                                    " đồng chẵn."
                                    : "N/A")
                             .Replace("{Content}", booking.Note);

            return htmlContent;
        }


        public void AddNew(Booking p)
        {
            _book.AddNew(p);
        }

        public List<Booking> GetBookingByBooked()
        {
            return _book.GetBookingByBooked();
        }

        public List<Booking> GetBookingByCheckedIn()
        {
            return _book.GetBookingByCheckedIn();
        }

        public Booking? GetBookingByDepositedTimed(int numberBooking)
        {
            return _book.GetBookingByDepositedTimed(numberBooking);
        }

        public Booking GetBookingById(Guid id)
        {
            return _book.GetBookingById(id);
        }

        public Booking? GetBookingByRandom(int numberBooking)
        {
            return _book.GetBookingByRandom(numberBooking);
        }

        public List<Booking> GetBookings()
        {
            return _book.GetBookings();
        }

        public void UpdateBooking(Booking p)
        {
            _book.UpdateBooking(p);
        }

        public List<Booking> GetBookingByDocumentID(Guid id)
        {
            return _book.GetBookingByDocumentID(id);
        }

        public List<Booking> GetBookingByCustomerID(Guid id)
        {
            return _book.GetBookingByCustomerID(id);
        }

        public Booking CheckExistingBooking(Guid openForSaleID, Guid categoryDetailID, Guid customerID)
        {
            return _book.CheckExistingBooking(openForSaleID, categoryDetailID, customerID);
        }

        public Booking? GetBookingByPropertyID(Guid propertyid)
        {
            return _book.GetBookingByPropertyID(propertyid);
        }
    }
}
