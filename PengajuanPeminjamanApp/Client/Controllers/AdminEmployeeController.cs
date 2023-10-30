using API.DTOs.Employees;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers
{
    public class AdminEmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public AdminEmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("admin/listEmployee")]
        public IActionResult ListEmployee()
        {
            return View();
        }

        [HttpGet("admin/getAllEmployee")]
        public async Task<JsonResult> GetAllEmployee()
        {
            var result = await _employeeRepository.Get();
            return Json(result);
        }

        [HttpPost("admin/insertEmployee")]
        public async Task<JsonResult> InsertEmployee(CreateEmployeeDto createEmployeeDto) 
        {
            var result = await _employeeRepository.InsertEmployee(createEmployeeDto);
            if (result.Code == 200)
            {
                return Json(result);
            }
            else if (result.Code == 400)
            {
                return Json(result);
            }
            return Json(result);
        }

        [Route("admin/employee/edit/{guid}")]
        public async Task<JsonResult> Edit(Guid guid)
        {
            var result = await _employeeRepository.Get(guid);
            var employee = new EmployeeDto();

            if (result.Data != null)
            {
                employee = (EmployeeDto)result.Data;
            }
            return Json(employee);
        }
    }
}
