using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class NotificationDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public NotificationDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<Notification> GetAllNotification()
        {
            try
            {
                return _context.Notifications!.Include(a => a.Booking)
                                              .Include(a => a.Customer)
                                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Notification GetNotificationByID(Guid id)
        {
            try
            {
                var notification = _context.Notifications!.Include(a => a.Booking)
                                                          .Include(a => a.Customer)
                                                          .SingleOrDefault(c => c.NotificationID == id);
                return notification;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Notification> GetNotificationByCustomerID(Guid customerId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Notifications.Include(a => a.Booking)
                                         .Include(a => a.Customer)
                                         .Where(a => a.CustomerID == customerId)
                                         .ToList();
        }

        public void AddNewNotification(Notification noti)
        {
            try
            {
                _context.Add(noti);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void UpdateNotification(Notification noti)
        {
            try
            {
                var a = _context.Notifications!.SingleOrDefault(c => c.NotificationID == noti.NotificationID);

                _context.Entry(a).CurrentValues.SetValues(noti);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusNotification(Notification noti)
        {
            var _noti = _context.Notifications!.FirstOrDefault(c => c.NotificationID.Equals(noti.NotificationID));


            if (_noti == null)
            {
                return false;
            }
            else
            {
                _noti.Status = false;
                _context.Entry(_noti).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }


    }
}
