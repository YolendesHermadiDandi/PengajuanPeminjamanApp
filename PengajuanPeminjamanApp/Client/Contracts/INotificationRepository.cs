using API.DTOs.Notifications;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface INotificationRepository : IRepository<NotificationDto, Guid>
    {
        Task<ResponseOKHandler<IEnumerable<NotificationDto>>> GetUnreadNotification(Guid guid);

        Task<ResponseOKHandler<NotificationDto>> UpdateNotification(Guid id);
    }
}
