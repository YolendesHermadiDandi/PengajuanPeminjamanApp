using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RequestRespository : GeneralRepository<Request>, IRequestRepository
{
	public RequestRespository(RequestFasilityDbContext context) : base(context)
	{

	}
}
