using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RequestRepository : GeneralRepository<Request>, IRequestRepository
{
	public RequestRepository(RequestFasilityDbContext context) : base(context)
	{

	}
}
