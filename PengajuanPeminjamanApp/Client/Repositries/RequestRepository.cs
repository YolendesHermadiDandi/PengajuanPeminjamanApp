using API.DTOs.Requests;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;

namespace Client.Repositries;

public class RequestRepository : GeneralRepository<RequestDto, Guid>, IRequestRepository
{
    public RequestRepository(string request = "Request/") : base(request)
    {
    }

    public async Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByEmployeeGuid(Guid guid)
    {
        ResponseOKHandler<IEnumerable<ListRequestDto>> entityVM;

        using (var response = await httpClient.GetAsync($"{request}GetRequestByEmployeeGuid?Guid={guid}"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<ListRequestDto>>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByDetailRequestGuid(Guid guid)
    {
        ResponseOKHandler<IEnumerable<ListRequestDto>> entityVM;

        using (var response = await httpClient.GetAsync(request+ "GetDetailRequestByGuid/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<ListRequestDto>>>(apiResponse);
        }
        return entityVM;
    }
}


