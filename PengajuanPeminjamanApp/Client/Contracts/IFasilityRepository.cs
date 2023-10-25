using API.DTOs.Employees;
using API.DTOs.Fasilities;
using API.DTOs.Notifications;
using API.Utilities.Handlers;
using System.Threading.Tasks;

namespace Client.Contracts
{
    public interface IFasilityRepository : IRepository<FasilityDto, Guid>
    {
        Task<ResponseOKHandler<FasilityDto>> Insert(CreateFasilityDto entity);
    }
}
