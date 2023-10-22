using API.DTOs.Roles;
using API.Models;

namespace API.DTOs.Notifications
{
    public class NotificationDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; }


        public static explicit operator NotificationDto(Notification notification)
        {
            return new NotificationDto
            {
                Guid = notification.Guid,
                AccountGuid = notification.AccountGuid,
                Message = notification.Massage,
                IsSeen = notification.IsSeen,
               
            };
        }

        public static implicit operator Notification(NotificationDto notificationDto)
        {
            return new Notification
            {
                Guid = notificationDto.Guid,
                AccountGuid = notificationDto.AccountGuid,
                Massage = notificationDto.Message,
                IsSeen = notificationDto.IsSeen,
              

            };
        }
    }
}
