using API.DTOs.Employees;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AdminDashboardController(INotificationRepository notificationRepository, IEmployeeRepository employeeRepository)
        {
            _notificationRepository = notificationRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/unreadNotification/{guid}")]
        public async Task<JsonResult> GetUnreadNotification(Guid guid)
        {
            var notif = await _notificationRepository.GetUnreadNotification(guid);
            List<EmployeeDto> employee = new List<EmployeeDto> { };
            foreach (var emp in notif.Data) 
            {
                var data = await _employeeRepository.Get(emp.AccountGuid);
                employee.Add(data.Data);
            }

            var result = new
            {
                notifications = notif.Data,
                employees = employee,    
            };
            return Json(result);
        }

        public async Task<JsonResult> UpdateNotification(Guid guid)
        {
            var result = await _notificationRepository.UpdateNotification(guid);
            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Updated - {result.Message}!";
                return Json(result);
            }
            return Json(result);
        }

        //public async Task<JsonResult> GetEmpData(Guid guid)
        //{
            
        //    return Json(result);
        //}


    }
}
