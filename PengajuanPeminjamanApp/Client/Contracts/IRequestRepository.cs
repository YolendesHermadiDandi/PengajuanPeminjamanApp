using API.DTOs.Requests;
using API.Utilities.Handlers;
using Client.Models;

namespace Client.Contracts;

public interface IRequestRepository : IRepository<RequestDto, Guid>
{
    Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByEmployeeGuid(Guid guid);
}
