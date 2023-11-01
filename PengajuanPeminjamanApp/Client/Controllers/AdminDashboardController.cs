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

        public AdminDashboardController(IEmployeeRepository employeeRepository, IRoomRepository roomRepository, IFasilityRepository facilityRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
            _facilityRepository = facilityRepository;
            _accountRepository = accountRepository;
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
            var room = await _roomRepository.Get();

            return Json(vasility);
        }
        
        [HttpGet("admin/getUserData")]
        public async Task<JsonResult> GetUserData()
        {
            var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
            return Json(result);
        }



    }
}
