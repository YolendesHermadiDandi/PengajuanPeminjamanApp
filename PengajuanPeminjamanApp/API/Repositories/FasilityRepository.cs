using API.Data;
using API.Model;

namespace API.Repositories;

public class FasilityRepository : GeneralRepository<Fasility>, IFasilityRespository
{
    public FasilityRepository(RequestFasilityDbContext context) : base(context)
	{
        
    }
}
