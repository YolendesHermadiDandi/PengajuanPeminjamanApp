using API.DTOs.Employees;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IEmployeeRepository : IRepository<EmployeeDto, Guid>
    {
        Task<ResponseOKHandler<CreateEmployeeDto>> InsertEmployee(CreateEmployeeDto entity);
    }
}
