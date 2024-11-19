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
    public class NotificationRepo : INotificationRepo
    {
        NotificationDAO dao = new NotificationDAO();

        public void AddNewNotification(Notification noti) => dao.AddNewNotification(noti);

        public bool ChangeStatusNotification(Notification noti) => dao.ChangeStatusNotification(noti);

        public List<Notification> GetAllNotification() => dao.GetAllNotification();

        public List<Notification> GetNotificationByCustomerID(Guid customerId) => dao.GetNotificationByCustomerID(customerId);

        public Notification GetNotificationByID(Guid id) => dao.GetNotificationByID(id);

        public void UpdateNotification(Notification noti) => dao.UpdateNotification(noti);

    }
}
