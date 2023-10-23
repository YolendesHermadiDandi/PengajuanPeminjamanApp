using API.Models;

namespace API.Contracts
{
	public interface IListFasilityRepository : IGeneralRepository<ListFasility>
	{
		IEnumerable<ListFasility>? GetAllListFasilityByReqGuid(Guid RequestGuid);
	}
}
