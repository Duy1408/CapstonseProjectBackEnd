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
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _book;
        private readonly IDocumentTemplateService _documentService;
        private readonly ICustomerServices _customerService;
        public BookingServices(IBookingRepo book, IDocumentTemplateService documentService, ICustomerServices customerService)
        {
            _book = book;
            _documentService = documentService;
            _customerService = customerService;
        }

        public string GenerateDocumentContent(Guid templateId)
        {
            var documentTemplate = _documentService.GetDocumentById(templateId);
            if (documentTemplate == null)
            {
                throw new Exception("Document template not found");
            }

            var booking = _book.GetBookingByDocumentID(templateId);
            var customer = _customerService.GetCustomerByID(booking.CustomerID);

            var htmlContent = documentTemplate.DocumentFile;

            htmlContent = htmlContent.Replace("{OwnerName}", customer.FullName)
                             .Replace("{OwnerBirthYear}", customer.DateOfBirth.ToString())
                             .Replace("{OwnerIdNumber}", customer.IdentityCardNumber)
                             .Replace("{OwnerAddress}", customer.Address)
                             .Replace("{OwnerPhone}", customer.PhoneNumber);

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

        public List<Booking> GetBookingByDepositedTimed(int numberBooking)
        {
            return _book.GetBookingByDepositedTimed(numberBooking);
        }

        public Booking GetBookingById(Guid id)
        {
            return _book.GetBookingById(id);
        }

        public List<Booking> GetBookingByRandom(int numberBooking)
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

        public Booking GetBookingByDocumentID(Guid id)
        {
            return _book.GetBookingByDocumentID(id);
        }

        public List<Booking> GetBookingByCustomerID(Guid id)
        {
            return _book.GetBookingByCustomerID(id);
        }

        public Booking CheckExistingBooking(Guid openForSaleID, Guid projectID, Guid customerID)
        {
            return _book.CheckExistingBooking(openForSaleID, projectID, customerID);
        }
    }
}
