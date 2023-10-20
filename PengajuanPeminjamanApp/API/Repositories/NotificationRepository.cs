using API.Contracts;
using API.Data;
using API.Model;

namespace API.Repositories;

public class NotificationRepository : GeneralRepository<Notification>, INotificationRepository
{
    public NotificationRepository(RequestFasilityDbContext context) : base(context)
	{
        
    }
}
