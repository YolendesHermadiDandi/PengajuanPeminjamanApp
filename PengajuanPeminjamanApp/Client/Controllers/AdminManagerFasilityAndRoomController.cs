using API.DTOs.Fasilities;
using API.DTOs.Rooms;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
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
            var fasility = await _fasilityRepository.Get();

            foreach (var item in fasility.Data)
            {
                if (item.Name.ToLower() == createFasilityDto.Name.ToLower() || createFasilityDto.Stock < 1)
                {
                    fasility.Code = 400;
                    return Json(fasility);
                }
            }


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
            var data = await _fasilityRepository.Get(fasility.Guid);

            var allFasility = await _fasilityRepository.Get();

            foreach (var item in allFasility.Data)
            {
                if (data.Data.Name != fasility.Name)
                {
                    if (item.Name.ToLower() == fasility.Name.ToLower() || fasility.Stock < 1)
                    {
                        allFasility.Code = 400;
                        return Json(allFasility);
                    }
                }
                else
                {
                    if(fasility.Stock < 1)
                    {
                        allFasility.Code = 400;
                        return Json(allFasility);
                    }
                }

            }
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

        [Authorize(Roles = "Admin")]
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

        [HttpGet("room/get/{guid}")]
        public async Task<JsonResult> Get(Guid guid)
        {
            var result = await _roomRepository.Get(guid);

            return Json(result);
        }

        [HttpPost("room/insert")]
        public async Task<JsonResult> InsertRoom(CreateRoomDto createRoomDto)
        {

            var room = await _roomRepository.Get();

            foreach (var item in room.Data)
            {
                if (item.Name.ToLower() == createRoomDto.Name.ToLower() || createRoomDto.Floor < 1)
                {
                    room.Code = 400;
                    return Json(room);
                }
            }

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
            var data = await _roomRepository.Get(roomDto.Guid);

            var allRoom = await _roomRepository.Get();

            foreach (var item in allRoom.Data)
            {
                if (data.Data.Name != roomDto.Name)
                {
                    if (item.Name.ToLower() == roomDto.Name.ToLower() || roomDto.Floor < 1)
                    {
                        allRoom.Code = 400;
                        return Json(allRoom);
                    }
                }
                else
                {
                    if (roomDto.Floor < 1)
                    {
                        allRoom.Code = 400;
                        return Json(allRoom);
                    }
                }

            }

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
