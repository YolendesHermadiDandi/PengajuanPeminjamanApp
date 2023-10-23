using API.DTOs.Employees;
using API.DTOs.Notifications;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IEmployeeRepository : IRepository<EmployeeDto, Guid>
    {
       
    }
}
