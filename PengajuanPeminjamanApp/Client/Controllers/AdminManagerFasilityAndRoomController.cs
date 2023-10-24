using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminManagerFasilityAndRoomController : Controller
    {


        [HttpGet("admin/list-fasilitas")]
        public IActionResult ListFasilitas()
        {
            return View();
        }
        [HttpGet("admin/list-ruangan")]
        public IActionResult ListRuangan()
        {
            return View();
        }
    }
}
