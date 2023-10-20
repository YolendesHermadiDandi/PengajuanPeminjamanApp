using API.Data;
using API.Model;

namespace API.Repositories;

public class NotificationRepository : GeneralRepository<Notification>, INotificationRepositroy
{
    public NotificationRepository(RequestFasilityDbContext context) : base(context)
	{
        
    }
}
