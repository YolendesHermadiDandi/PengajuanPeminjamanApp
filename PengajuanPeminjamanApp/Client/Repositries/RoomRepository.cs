using API.DTOs.Employees;
using API.DTOs.Fasilities;
using API.DTOs.Rooms;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositries
{
    public class RoomRepository : GeneralRepository<RoomDto, Guid>, IRoomRepository
    {

        public RoomRepository(string request="room/") : base(request) { }

        public async Task<ResponseOKHandler<RoomDto>> Insert(CreateRoomDto entity)
        {
            ResponseOKHandler<RoomDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<RoomDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
