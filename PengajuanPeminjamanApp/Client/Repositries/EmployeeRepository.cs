using API.DTOs.Employees;
using API.DTOs.Employees;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositries
{
    public class EmployeeRepository : GeneralRepository<EmployeeDto, Guid>, IEmployeeRepository
    {

        public EmployeeRepository(string request="employee/") : base(request) { }

    }
}
