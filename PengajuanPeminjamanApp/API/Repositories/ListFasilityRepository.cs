using API.Data;
using API.Model;

namespace API.Repositories;

public class ListFasilityRepository : GeneralRepository<ListFasility>, IListFasility
{
    public ListFasilityRepository(RequestFasilityDbContext context) : base(context)
	{
        
    }
}
