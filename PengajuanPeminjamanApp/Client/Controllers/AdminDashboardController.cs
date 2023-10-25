using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public AdminDashboardController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<JsonResult> GetEmpData(Guid guid)
        //{

        //    return Json(result);
        //}


    }
}
