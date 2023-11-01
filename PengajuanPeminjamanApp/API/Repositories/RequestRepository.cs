using API.Contracts;
using API.Data;
using API.DTOs.Requests;
using API.Models;
using API.Utilities.Handlers;

namespace API.Repositories;

public class RequestRepository : GeneralRepository<Request>, IRequestRepository
{
    public RequestRepository(RequestFasilityDbContext context) : base(context)
    {

    }

    public IEnumerable<Request> GetRequestByEmployeeGuid(Guid employeeGuid)
    {
        return _context.Set<Request>().Where(rq => rq.EmployeeGuid == employeeGuid).ToList();
    }

    public bool GetStatusRequestRoomByDate(StatusRequestRoomDto entity)
    {
        try
        {

            var requestStatusList = _context.Set<Request>()
            .Where(rq => rq.RoomGuid == entity.roomGuid)
            .ToList();

            foreach (var room in requestStatusList)
            {
                if ((entity.startDate >= room.StartDate && entity.startDate <= room.EndDate) ||
                    (entity.endDate >= room.StartDate && entity.endDate <= room.EndDate) ||
                    (entity.startDate <= room.StartDate && entity.endDate >= room.EndDate))
                {
                    // Terdapat tumpang tindih, maka booking gagal
                    return false;
                }
            }
            _context.SaveChanges();
            return true;

        }
        catch (Exception ex)
        {
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
        }
    }
}
