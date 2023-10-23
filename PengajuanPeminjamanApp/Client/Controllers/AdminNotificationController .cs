using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers
{
    public class AdminNotificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AdminNotificationController(INotificationRepository notificationRepository, IEmployeeRepository employeeRepository)
        {
            _notificationRepository = notificationRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("admin/notifications")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/allNotification/{guid}")]
        public async Task<JsonResult> GetAllNotification(Guid guid)
        {
            var notif = await _notificationRepository.GetAllNotification(guid);
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

    }
}
