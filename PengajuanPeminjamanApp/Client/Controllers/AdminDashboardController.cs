using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IFasilityRepository _facilityRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRequestRepository _requestRepository;

        public AdminDashboardController(IEmployeeRepository employeeRepository, IRoomRepository roomRepository, IFasilityRepository facilityRepository, IAccountRepository accountRepository, IRequestRepository requestRepository)
        {
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
            _facilityRepository = facilityRepository;
            _accountRepository = accountRepository;
            _requestRepository = requestRepository;
        }

        [HttpGet("admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet("admin/geAllDataFasikit&Room")]
        public async Task<JsonResult> GetAllFasilityAndRoom()
        {
            var vasility = await _facilityRepository.Get();
            var statusCount = await _requestRepository.GetCountStatusRequest();

            var result = new
            {
                vasility = vasility.Data,
                statusCount = statusCount.Data,
            };

            //var room = await _roomRepository.Get();

            return Json(result);
        }

        [HttpGet("admin/getUserData")]
        public async Task<JsonResult> GetUserData()
        {
            var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
            return Json(result);
        }



    }
}
