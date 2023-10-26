using API.DTOs.Requests;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

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

        using (var response = await httpClient.GetAsync(request + "GetDetailRequestByGuid/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<ListRequestDto>>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseOKHandler<UpdateStatusDto>> UpdateRequestStatus(UpdateStatusDto entity)
    {
        ResponseOKHandler<UpdateStatusDto> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = httpClient.PutAsync(request + "updateStatus", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<UpdateStatusDto>>(apiResponse);
        }
        return entityVM;
    }
    public async Task<ResponseOKHandler<SendEmailDto>> SendEmail(SendEmailDto entity)
    {
        ResponseOKHandler<SendEmailDto> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "sendEmail", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<SendEmailDto>>(apiResponse);
        }
        return entityVM;
    }
}


