using API.Models;

namespace API.Contracts
{
    public interface INotificationRepository : IGeneralRepository<Notification>
    {
        IEnumerable<Notification> GetUnreadNotification(Guid empGuid);
        IEnumerable<Notification> GetAllNotification(Guid empGuid);
    }
}
