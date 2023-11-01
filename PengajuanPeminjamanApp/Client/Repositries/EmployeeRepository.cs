using API.DTOs.Employees;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositries
{
    public class EmployeeRepository : GeneralRepository<EmployeeDto, Guid>, IEmployeeRepository
    {

        public EmployeeRepository(string request = "employee/") : base(request) { }

        public async Task<ResponseOKHandler<CreateEmployeeDto>> InsertEmployee(CreateEmployeeDto entity)
        {
            ResponseOKHandler<CreateEmployeeDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "insert", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<CreateEmployeeDto>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<EmployeeDto>> UpdateEmployee(EmployeeDto entity)
        {
            ResponseOKHandler<EmployeeDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request + "update", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<EmployeeDto>>(apiResponse);
            }
            return entityVM;
        }

    }
}
