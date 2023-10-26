using API.DTOs.Fasilities;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IFasilityRepository : IRepository<FasilityDto, Guid>
    {
        Task<ResponseOKHandler<FasilityDto>> Insert(CreateFasilityDto entity);
    }
}
