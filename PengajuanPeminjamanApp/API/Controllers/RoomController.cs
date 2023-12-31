﻿using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("API/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRequestRepository _requestRepository;

    public RoomController(IRoomRepository roomRepository, IRequestRepository requestRepository)
    {
        _roomRepository = roomRepository;
        _requestRepository = requestRepository;
    }

    [HttpGet("GetRoomStatus")]
    public IActionResult GetRoomIdle()
    {
        var room = _roomRepository.GetAll();
        var requests = _requestRepository.GetAll();

        if (!(requests.Any() && room.Any()))
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No Room Is Empty"
            });
        }

        var result = from roo in room
                     join req in requests on roo.Guid equals req.RoomGuid into roomRequests
                     from req in roomRequests.DefaultIfEmpty()
                     select new RoomStatusDto
                     {
                         Guid = roo.Guid,
                         Name = roo.Name,
                         Floor = roo.Floor,
                         Status = req != null ? req.Status : 0
                     };

        return Ok(new ResponseOKHandler<IEnumerable<RoomStatusDto>>(result));
    }

    [HttpGet("GetRoomDate")]
    public IActionResult GetRoomDate()
    {
        var room = _roomRepository.GetAll();
        var requests = _requestRepository.GetAll();

        if (!(requests.Any() && room.Any()))
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No Room Is Empty"
            });
        }

        IEnumerable<RoomDateRequestDto> result = (from roo in room
                                                  join req in requests on roo.Guid equals req.RoomGuid into roomRequests
                                                  from req in roomRequests where req.Status != StatusLevel.Rejected
                                                  select new RoomDateRequestDto
                                                  {
                                                      title = "Ruangan " + roo.Name,
                                                      start = req.StartDate,
                                                      end = req.EndDate.AddDays(1)
                                                  }).ToList();

        return Ok(new ResponseOKHandler<IEnumerable<RoomDateRequestDto>>(result));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roomRepository.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        var data = result.Select(x => (RoomDto)x);
        return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>(data));
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        return Ok(new ResponseOKHandler<RoomDto>((RoomDto)result));
    }

    [HttpPost]
    public IActionResult Create(CreateRoomDto roomDto)
    {
        try
        {
            var result = _roomRepository.Create(roomDto);
            return Ok(new ResponseOKHandler<RoomDto>((RoomDto)result));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to create data",
                Error = ex.Message
            });
        }
    }

    [HttpPut]
    public IActionResult Update(RoomDto roomDto)
    {
        try
        {
            var entity = _roomRepository.GetByGuid(roomDto.Guid);
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            Room toUpdate = (Room)roomDto;
            toUpdate.CreateDate = entity.CreateDate;
            var result = _roomRepository.Update(toUpdate);
            return Ok(new ResponseOKHandler<string>("Success Update Data"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }

    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _roomRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            var result = _roomRepository.Delete(entity);
            return Ok(new ResponseOKHandler<string>("Success Delete Data"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }
    }
}
