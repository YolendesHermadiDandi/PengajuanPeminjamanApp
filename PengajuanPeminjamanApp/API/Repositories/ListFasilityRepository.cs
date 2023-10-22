using API.Contracts;
using API.Data;
using API.Models;
using System;

namespace API.Repositories;

public class ListFasilityRepository : GeneralRepository<ListFasility>, IListFasilityRepository
{
    public ListFasilityRepository(RequestFasilityDbContext context) : base(context)
    {

    }

    public IEnumerable<ListFasility> GetAllListFasilityByReqGuid(Guid RequestGuid)
    {
		return _context.Set<ListFasility>().Where(d => d.RequestGuid == RequestGuid).ToList();
		
	}
}
