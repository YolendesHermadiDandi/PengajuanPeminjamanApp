
using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
using API.DTOs.Requests;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeDateController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public EmployeeDateController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet("room/GetRoomDate")]
        public async Task<JsonResult> GetRoomDate()
        {
            var room = await _roomRepository.GetRoomDate();
            return Json(room.Data);
        }

    }
}
