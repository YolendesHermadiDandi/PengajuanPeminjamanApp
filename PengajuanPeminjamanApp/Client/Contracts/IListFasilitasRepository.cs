using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IListFasilitasRepository : IRepository<ListFasilityDto, Guid>
    {
        Task<ResponseOKHandler<ListFasilityDto>> Insert(CreateListFasilityDto entity);
    }
}
