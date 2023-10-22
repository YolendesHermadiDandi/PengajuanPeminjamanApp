using API.DTOs.Roles;
using API.Models;

namespace API.DTOs.Notifications
{
    public class CreateNotificationDto
    {
        public Guid AccountGuid { get; set; }
        public string Message { get; set; }
        public Guid RequestGuid { get; set; }
        //public bool IsSeen {  get; set; }

        public static implicit operator Notification(CreateNotificationDto createNotificationDto)
        {
            return new Notification
            {
                AccountGuid = createNotificationDto.AccountGuid,
                Massage = createNotificationDto.Message,
                RequestGuid = createNotificationDto.RequestGuid,
                IsSeen = false,
                CreatedDate = DateTime.Now,
                
               
            };
        }
    }
}
