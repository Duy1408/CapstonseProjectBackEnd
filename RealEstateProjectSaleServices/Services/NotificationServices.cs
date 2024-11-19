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
    public class NotificationServices : INotificationServices
    {
        private readonly INotificationRepo _notiRepo;
        public NotificationServices(INotificationRepo notiRepo)
        {
            _notiRepo = notiRepo;
        }
        public void AddNewNotification(Notification noti) => _notiRepo.AddNewNotification(noti);

        public bool ChangeStatusNotification(Notification noti) => _notiRepo.ChangeStatusNotification(noti);

        public List<Notification> GetAllNotification() => _notiRepo.GetAllNotification();

        public List<Notification> GetNotificationByCustomerID(Guid customerId) => _notiRepo.GetNotificationByCustomerID(customerId);

        public Notification GetNotificationByID(Guid id) => _notiRepo.GetNotificationByID(id);

        public void UpdateNotification(Notification noti) => _notiRepo.UpdateNotification(noti);

    }
}
