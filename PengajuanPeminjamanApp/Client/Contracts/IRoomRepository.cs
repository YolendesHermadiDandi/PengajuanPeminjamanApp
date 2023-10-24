using API.DTOs.Employees;
using API.DTOs.Fasilities;
using API.DTOs.Notifications;
using API.DTOs.Rooms;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IRoomRepository : IRepository<RoomDto, Guid>
    {
        Task<ResponseOKHandler<RoomDto>> Insert(CreateRoomDto entity);
    }
}
