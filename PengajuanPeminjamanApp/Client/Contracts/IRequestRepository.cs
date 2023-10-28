using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
using API.DTOs.Requests;
using API.Utilities.Handlers;

namespace Client.Contracts;

public interface IRequestRepository : IRepository<RequestDto, Guid>
{
    Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByEmployeeGuid(Guid guid);
    Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByDetailRequestGuid(Guid guid);
    Task<ResponseOKHandler<RequestDto>> Insert(CreateRequestDto entity);
    Task<ResponseOKHandler<IEnumerable<CountRequestStatusDto>>> GetCountStatusRequestByEmployeeGuid(Guid guid);

    Task<ResponseOKHandler<UpdateStatusDto>> UpdateRequestStatus(UpdateStatusDto entity);
    Task<ResponseOKHandler<SendEmailDto>> SendEmail(SendEmailDto entity);

    Task<ResponseOKHandler<ListFasilityDto>> UpdateStokFasility(ListFasilityDto entity);
}
