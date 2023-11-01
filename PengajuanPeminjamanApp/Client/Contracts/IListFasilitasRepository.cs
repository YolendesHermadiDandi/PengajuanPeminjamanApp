using API.DTOs.ListFasilities;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IListFasilitasRepository : IRepository<ListFasilityDto, Guid>
    {
        Task<ResponseOKHandler<ListFasilityDto>> Insert(CreateListFasilityDto entity);
        Task<ResponseOKHandler<ListFasilityDto>> GetListFasilityByRequestGuidAndFasilityGuid(FindListFasilityDto findListFasility);
        Task<ResponseOKHandler<IEnumerable<ListFasilityDto>>> GetListFasilityByRequestGuid(Guid guid);
    }
}
