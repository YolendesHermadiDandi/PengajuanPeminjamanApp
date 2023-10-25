using API.DTOs.Employees;
using API.DTOs.Fasilities;
using API.DTOs.Notifications;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IFasilityRepository : IRepository<FasilityDto, Guid>
    {
       
    }
}
