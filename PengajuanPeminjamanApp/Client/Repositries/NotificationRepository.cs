using API.DTOs.Employees;
using API.DTOs.Notifications;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositries
{
    public class NotificationRepository : GeneralRepository<NotificationDto, Guid>, INotificationRepository
    {

        public NotificationRepository(string request="notifikasi/") : base(request) { }

        public async Task<ResponseOKHandler<IEnumerable<NotificationDto>>> GetUnreadNotification(Guid guid)
        {
            ResponseOKHandler<IEnumerable<NotificationDto>> entityVM = null;

            using (var response = await httpClient.GetAsync(request+ "unreadEmpNotification/" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<NotificationDto>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<NotificationDto>> UpdateNotification(Guid id)
        {
            ResponseOKHandler<NotificationDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request + id, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<NotificationDto>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<IEnumerable<NotificationDto>>> GetAllNotification(Guid guid)
        {
            ResponseOKHandler<IEnumerable<NotificationDto>> entityVM = null;

            using (var response = await httpClient.GetAsync(request + "allEmpNotification/" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<NotificationDto>>>(apiResponse);
            }
            return entityVM;
        }
    }
}
