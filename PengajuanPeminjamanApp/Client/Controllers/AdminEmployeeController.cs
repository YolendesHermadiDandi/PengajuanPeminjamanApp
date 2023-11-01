using API.DTOs.Accounts;
using API.DTOs.Employees;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminEmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;

        public AdminEmployeeController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
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
        public async Task<JsonResult> InsertEmployee(RegisterAccountDto registerAccountDto)
        {
            var result = await _accountRepository.RegisterEmployee(registerAccountDto);
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

        [HttpPost("admin/employee/update")]
        public async Task<JsonResult> Update(EmployeeDto emp)
        {
            var result = await _employeeRepository.UpdateEmployee(emp);

            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }


    }
}
