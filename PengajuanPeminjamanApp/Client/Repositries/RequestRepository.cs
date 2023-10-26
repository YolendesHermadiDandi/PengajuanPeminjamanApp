using API.DTOs.Requests;
using API.Models;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Models;
using Client.Repositories;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Repositries;

public class RequestRepository : GeneralRepository<RequestDto, Guid>, IRequestRepository
{
    public RequestRepository(string request = "Request/") : base(request)
    {
    }

    public async Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByEmployeeGuid(Guid guid)
    {
        ResponseOKHandler<IEnumerable<ListRequestDto>> entityVM ;

        using (var response = await httpClient.GetAsync($"{request}GetRequestByEmployeeGuid?Guid={guid}"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<ListRequestDto>>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseOKHandler<IEnumerable<ListRequestDto>>> GetByRequestGuid(Guid guid)
    {
        ResponseOKHandler<IEnumerable<ListRequestDto>> entity = null;

        using (var response = await httpClient.GetAsync(request + "GetDetailRequestByGuid?guid=" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<ListRequestDto>>>(apiResponse);
        }
        return entity;
    }
}
