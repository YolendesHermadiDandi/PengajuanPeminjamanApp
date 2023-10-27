using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
using API.DTOs.Requests;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositries
{
    public class ListFasilitasRepository : GeneralRepository<ListFasilityDto, Guid>, IListFasilitasRepository
    {
        public ListFasilitasRepository(string request = "ListFasility/") : base(request) { }

        public async Task<ResponseOKHandler<ListFasilityDto>> GetListFasilityByRequestGuidAndFasilityGuid(FindListFasilityDto entity)
        {
            ResponseOKHandler<ListFasilityDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request+ "GetListFasilityByReqGuidAndFasilityGuid", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<ListFasilityDto>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<IEnumerable<ListFasilityDto>>> GetListFasilityByRequestGuid(Guid guid)
        {
            ResponseOKHandler<IEnumerable<ListFasilityDto>> entityVM;

            using (var response = await httpClient.GetAsync(request + "GetListFasilityByRequestGuid/" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<ListFasilityDto>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<ListFasilityDto>> Insert(CreateListFasilityDto entity)
        {
            ResponseOKHandler<ListFasilityDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<ListFasilityDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
