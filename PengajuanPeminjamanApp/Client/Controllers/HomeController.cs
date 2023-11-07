using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRequestRepository _requestRepository;
        public HomeController(ILogger<HomeController> logger, IRequestRepository requestRepository)
        {
            _logger = logger;
            _requestRepository = requestRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("qrScan/getRequest/{id}")]
        public async Task<JsonResult> GetDetailRequestGuid(Guid id)
        {
            var result = await _requestRepository.Get(id);
            if (result.Data is null)
            {
                result.Code = 404;
            }
            else if (result.Data.EndDate < DateTime.Now && result.Code != 404)
            {
                result.Code = 410;
            }
            return Json(result);
        }


    }
}