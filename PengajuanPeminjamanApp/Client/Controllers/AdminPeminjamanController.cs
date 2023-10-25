using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminPeminjamanController : Controller
    {


        [HttpGet("admin/listPeminjaman")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
