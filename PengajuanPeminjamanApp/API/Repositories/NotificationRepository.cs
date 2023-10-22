using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class NotificationRepository : GeneralRepository<Notification>, INotificationRepository
{
    public NotificationRepository(RequestFasilityDbContext context) : base(context)
    {

    }

    public IEnumerable<Notification> GetUnreadNotification(Guid empGuid)
    {
        return _context.Set<Notification>().Where(n => n.IsSeen == false && n.AccountGuid == empGuid ).ToList();
    }

    public IEnumerable<Notification> GetAllNotification(Guid empGuid)
    {
        return _context.Set<Notification>().Where(n => n.AccountGuid == empGuid).ToList();
    }
}
