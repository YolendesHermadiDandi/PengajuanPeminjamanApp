using API.DTOs.Employees;
using API.DTOs.Requests;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminPeminjamanController : Controller
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AdminPeminjamanController(IRequestRepository requestRepository, IEmployeeRepository employeeRepository)
        {
            _requestRepository = requestRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("admin/listPeminjaman")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("request/get-all")]
        public async Task<JsonResult> GetAlletails()
        {

            var request = await _requestRepository.Get();
            List<EmployeeDto> employee = new List<EmployeeDto> { };
            List<object> listPeminjaman = new List<object>();
            foreach (var emp in request.Data)
            {
                var data = await _employeeRepository.Get(emp.EmployeeGuid);
                employee.Add(data.Data);
                var result = new
                {
                    nama = data.Data.FirstName + " " + data.Data.LastName,
                    status = emp.Status.ToString(),
                    requestGuid = emp.Guid,
                };
                listPeminjaman.Add(result);
            }
            return Json(listPeminjaman);
        }

        [Route("request/get-details/{id}")]
        public async Task<JsonResult> GetDetailRequestGuid(Guid id)
        {
            var request = await _requestRepository.GetByDetailRequestGuid(id);
            var employee = await _employeeRepository.Get(request.Data.FirstOrDefault().EmployeeGuid);
            var result = new
            {
                email = employee.Data.Email,
                nama = employee.Data.FirstName + " " + employee.Data.LastName,
                request = request,
                requestStatus = request.Data.FirstOrDefault().Status.ToString(),
            };
            return Json(result);
        }


        [HttpPost("request/statusUpdate")]
        public async Task<JsonResult> UpdateStatusRequest(UpdateStatusDto updateStatusDto)
        {
            var result = await _requestRepository.UpdateRequestStatus(updateStatusDto);

            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }

        [HttpPost("request/sendEmail")]
        public async Task<JsonResult> SendEmail(SendEmailDto sendEmailDto)
        {


            var result = await _requestRepository.SendEmail(sendEmailDto);
            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }

    }
}
