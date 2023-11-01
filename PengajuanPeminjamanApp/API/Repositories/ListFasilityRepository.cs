using API.Contracts;
using API.Data;
using API.DTOs.ListFasilities;
using API.Models;

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

    public ListFasility GetListFasilityByReqGuidAndFasilityGuid(FindListFasilityDto findListFasility)
    {
        var entity = _context.Set<ListFasility>().FirstOrDefault(lf => lf.FasilityGuid == findListFasility.FasilityGuid && lf.RequestGuid == findListFasility.RequestGuid && lf.Guid == findListFasility.guid);
        _context.ChangeTracker.Clear();
        return entity;
    }
}
