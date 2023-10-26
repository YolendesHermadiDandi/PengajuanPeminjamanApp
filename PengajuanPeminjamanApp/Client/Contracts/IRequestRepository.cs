﻿using API.DTOs.Requests;
using API.Utilities.Handlers;

namespace Client.Contracts;

public interface IRequestRepository : IRepository<RequestDto, Guid>
{
    Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByEmployeeGuid(Guid guid);
    Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByDetailRequestGuid(Guid guid);

}