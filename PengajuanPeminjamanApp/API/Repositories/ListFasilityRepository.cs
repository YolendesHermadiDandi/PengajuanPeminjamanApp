using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class ListFasilityRepository : GeneralRepository<ListFasility>, IListFasilityRepository
{
    public ListFasilityRepository(RequestFasilityDbContext context) : base(context)
    {

    }
}
