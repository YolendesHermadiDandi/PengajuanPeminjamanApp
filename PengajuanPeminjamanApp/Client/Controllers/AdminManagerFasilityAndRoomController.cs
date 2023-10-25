using API.DTOs.Fasilities;
using API.DTOs.Rooms;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AdminManagerFasilityAndRoomController : Controller
    {
        private readonly IFasilityRepository _fasilityRepository;
        private readonly IRoomRepository _roomRepository;

        public AdminManagerFasilityAndRoomController(IFasilityRepository fasilityRepository, IRoomRepository roomRepository)
        {
            _fasilityRepository = fasilityRepository;
            _roomRepository = roomRepository;
        }



        //Start fasility
        [HttpGet("admin/list-fasilitas")]
        public IActionResult ListFasilitas()
        {
            return View();
        }

        [HttpGet("fasility/get-all")]
        public async Task<JsonResult> GetAllFasility()
        {
            var result = await _fasilityRepository.Get();

            return Json(result);
        }

        [HttpPost("fasility/insert")]
        public async Task<JsonResult> InsertFasility(CreateFasilityDto createFasilityDto)
        {
            var result = await _fasilityRepository.Insert(createFasilityDto);
            if (result.Code == 200)
            {
                return Json(result);
            }
            else if (result.Code == 400)
            {
                return Json(result);
            }
            return Json(result);
        }

        [Route("fasility/edit/{guid}")]
        public async Task<JsonResult> EditFasility(Guid guid)
        {
            var result = await _fasilityRepository.Get(guid);
            var fasility = new FasilityDto();

            if (result.Data != null)
            {
                fasility = (FasilityDto)result.Data;
            }
            return Json(fasility);
        }

        [HttpPost("fasility/update")]
        public async Task<JsonResult> UpdateFasility(FasilityDto fasility)
        {
            var result = await _fasilityRepository.Put(fasility.Guid, fasility);

            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }

        [Route("fasility/delete/{guid}")]
        public async Task<JsonResult> DeleteFasity(Guid guid)
        {
            var result = await _fasilityRepository.Delete(guid);

            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }


        //End of fasility

        //Start Room

        [HttpGet("admin/list-ruangan")]
        public IActionResult ListRuangan()
        {
            return View();
        }

        [HttpGet("room/get-all")]
        public async Task<JsonResult> GetAllRoom()
        {
            var result = await _roomRepository.Get();

            return Json(result);
        }

        [HttpPost("room/insert")]
        public async Task<JsonResult> InsertRoom(CreateRoomDto createRoomDto)
        {
            var result = await _roomRepository.Insert(createRoomDto);
            if (result.Code == 200)
            {
                return Json(result);
            }
            else if (result.Code == 400)
            {
                return Json(result);
            }
            return Json(result);
        }

        [Route("room/edit/{guid}")]
        public async Task<JsonResult> EditRoom(Guid guid)
        {
            var result = await _roomRepository.Get(guid);
            var fasility = new RoomDto();

            if (result.Data != null)
            {
                fasility = (RoomDto)result.Data;
            }
            return Json(fasility);
        }

        [HttpPost("room/update")]
        public async Task<JsonResult> UpdateRoom(RoomDto roomDto)
        {
            var result = await _roomRepository.Put(roomDto.Guid, roomDto);

            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }

        [Route("room/delete/{guid}")]
        public async Task<JsonResult> DeleteRoom(Guid guid)
        {
            var result = await _roomRepository.Delete(guid);

            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }

        //End of room
    }
}
