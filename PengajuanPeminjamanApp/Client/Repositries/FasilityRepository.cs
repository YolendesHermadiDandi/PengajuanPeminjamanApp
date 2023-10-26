using API.DTOs.Fasilities;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositries
{
    public class FasilityRepository : GeneralRepository<FasilityDto, Guid>, IFasilityRepository
    {
        public FasilityRepository(string request = "fasility/") : base(request) { }

        public async Task<ResponseOKHandler<FasilityDto>> Insert(CreateFasilityDto entity)
        {
            ResponseOKHandler<FasilityDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<FasilityDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
