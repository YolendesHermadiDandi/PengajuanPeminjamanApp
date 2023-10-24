using API.Contracts;
using API.Data;
using API.Models;

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
}
