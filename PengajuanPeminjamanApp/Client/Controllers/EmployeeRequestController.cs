
using API.DTOs.Fasilities;
using API.DTOs.Requests;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeRequestController : Controller
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeRequestController(IRequestRepository requestRepository, IEmployeeRepository employeeRepository)
        {
            _requestRepository = requestRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("request/getRequestbyRequestGuid")]
        public async Task<JsonResult> GetRequestByRequestGuid(Guid guid)
        {
            var result = await _requestRepository.GetByRequestGuid(guid);
            var request = new ListRequestDto();

            if (result.Data != null)
            {
                //request = (ListRequestDto)result.Data;
            }
            return Json(result.Data);
        }
        
        [Route("employee/getEmployee/{guid}")]
        public async Task<JsonResult> GetEmployeeByGuid(Guid guid)
        {
            var result = await _employeeRepository.Get(guid);
            return Json(result.Data);
        }        
    }
}
