using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface INotificationRepo
    {
        List<Notification> GetAllNotification();
        Notification GetNotificationByID(Guid id);
        void AddNewNotification(Notification noti);
        void UpdateNotification(Notification noti);
        bool ChangeStatusNotification(Notification noti);
        List<Notification> GetNotificationByCustomerID(Guid customerId);
    }
}
