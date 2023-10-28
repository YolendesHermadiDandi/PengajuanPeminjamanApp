using API.DTOs.Requests;
using API.Models;

namespace API.Contracts
{
    public interface IRequestRepository : IGeneralRepository<Request>
    {
        IEnumerable<Request> GetRequestByEmployeeGuid(Guid employeeGuid);

        bool GetStatusRequestRoomByDate (StatusRequestRoomDto entity);
    }
}
