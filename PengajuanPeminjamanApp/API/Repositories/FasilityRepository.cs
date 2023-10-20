using API.Contracts;
using API.Data;
using API.Model;

namespace API.Repositories;

public class FasilityRepository : GeneralRepository<Fasility>, IFasilityRepository
{
	public FasilityRepository(RequestFasilityDbContext context) : base(context)
	{

	}
}
