using API.Contracts;
using API.DTOs.ListFasilities;
using API.DTOs.Requests;
using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("API/[controller]")]
public class RequestController : ControllerBase
{
	private readonly IRequestRepository _requestRespository;
	private readonly IFasilityRepository _fasilityRepository;
	private readonly IListFasilityRepository _listFasilityRepository;
	private readonly IRoomRepository _roomRepository;

	public RequestController(IRequestRepository requestRespository, IFasilityRepository fasilityRepository, IListFasilityRepository listFasilityRepository, IRoomRepository roomRepository)
	{
		_requestRespository = requestRespository;
		_fasilityRepository = fasilityRepository;
		_listFasilityRepository = listFasilityRepository;
		_roomRepository = roomRepository;
	}

	[HttpGet]
	public IActionResult GetAll()
	{
		var result = _requestRespository.GetAll();
		if (!result.Any())
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}
		var data = result.Select(x => (RequestDto)x);
		return Ok(new ResponseOKHandler<IEnumerable<RequestDto>>(data));
	}

	[HttpGet("GetCountingRequest")]
	public IActionResult GetRequestDuration(Guid guid)
	{
		var result = _requestRespository.GetByGuid(guid);
		if (result is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		var view = new DurationRequestDto
		{
			Guid = result.Guid,
			RoomGuid = result.RoomGuid,
			EmployeeGuid = result.EmployeeGuid,
			Status = result.Status,
			RequestDuration = DurationCalculator(result.StartDate, result.EndDate)
		};

		return Ok(new ResponseOKHandler<DurationRequestDto>((DurationRequestDto)view));
	}

	[HttpGet("{guid}")]
	public IActionResult GetByGuid(Guid guid)
	{
		var result = _requestRespository.GetByGuid(guid);
		if (result is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		return Ok(new ResponseOKHandler<RequestDto>((RequestDto)result));
	}

	[HttpGet("GetRequestByEmployeeGuid")]
	public IActionResult GetRequestByEmployeeGuid(Guid guid)
	{
		var result = _requestRespository.GetRequestByEmployeeGuid(guid);
		var room = _roomRepository.GetAll();
		if (result is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		List<ListRequestDto> data = (from req in result
				   where req.EmployeeGuid == guid
				   select new ListRequestDto
				   {
					   Guid = req.Guid,
					   EmployeeGuid = req.EmployeeGuid,
					   rooms = (from roo in room
							  join re in result on roo.Guid equals re.RoomGuid
							  where roo.Guid == req.RoomGuid
							  select new RoomDto
							  {
								  Guid = roo.Guid,
								  Name = roo.Name,
								  Floor = roo.Floor
							  }).FirstOrDefault(),
					   Status = req.Status,
					   StartDate = req.StartDate,
					   EndDate = req.EndDate
				   }).ToList();

        return Ok(new ResponseOKHandler<IEnumerable<ListRequestDto>>(data));
    }

	[HttpGet("GetAllDetailRequest")]
	public IActionResult GetAllDetailRequest()
	{
		var request = _requestRespository.GetAll();
		var listFasility = _listFasilityRepository.GetAll();
		var fasilities = _fasilityRepository.GetAll();
		var room = _roomRepository.GetAll();
		if (request is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

        List<ListDetailRequestDto> result = (from req in request
                                             select new ListDetailRequestDto
                                             {
                                                 Guid = req.Guid,
                                                 rooms = (from roo in room
                                                          join re in request on roo.Guid equals re.RoomGuid
                                                          where roo.Guid == req.RoomGuid
                                                          select new RoomDto
                                                          {
                                                              Name = roo.Name,
                                                              Floor = roo.Floor
                                                          }).FirstOrDefault(),
                                                 fasility = (from fas in fasilities
                                                             join li in listFasility on fas.Guid equals li.FasilityGuid
                                                             where li.RequestGuid == req.Guid
                                                             select new ListDetailFasilityDto
                                                             {
                                                                 Name = fas.Name,
                                                                 TotalFasility = li.TotalFasility
                                                             }).ToList(),
                                                 Status = req.Status,
                                                 StartDate = req.StartDate,
                                                 EndDate = req.EndDate,
                                             }).ToList();

        return Ok(new ResponseOKHandler<IEnumerable<ListDetailRequestDto>>(result));
	}
	
	[HttpGet("GetDetailRequestByGuid")]
	public IActionResult GetDetailRequestGuid(Guid guid)
	{
		var request = _requestRespository.GetAll();
		var listFasility = _listFasilityRepository.GetAll();
		var fasilities = _fasilityRepository.GetAll();
		var room = _roomRepository.GetAll();
		if (request is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		List<ListRequestDto> result = (from req in request
									   where req.Guid == guid
                      select new ListRequestDto
                      {
                          Guid = req.Guid,
                          EmployeeGuid = req.EmployeeGuid,
                          rooms = (from roo in room
                                   join re in request on roo.Guid equals re.RoomGuid
                                   where roo.Guid == req.RoomGuid
                                   select new RoomDto
                                   {
                                       Guid = roo.Guid,
                                       Name = roo.Name,
                                       Floor = roo.Floor
                                   }).FirstOrDefault(),
                          fasilities = from fas in fasilities
                                       join li in listFasility on fas.Guid equals li.FasilityGuid
                                       where li.RequestGuid == req.Guid
                                       select new ListDetailFasilityDto
                                       {
                                           Name = fas.Name,
                                           TotalFasility = li.TotalFasility
                                       },
                          Status = req.Status,
                          StartDate = req.StartDate,
                          EndDate = req.EndDate,
                      }).ToList();

        return Ok(new ResponseOKHandler<IEnumerable<ListRequestDto>>(result));
	}
	[HttpPut]
	public IActionResult Update(RequestDto requestDto)
	{
		try
		{
			var check = _requestRespository.GetByGuid(requestDto.Guid);
			if (check is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Data Not Found"
				});
			}
			Request toUpdate = (Request)requestDto;
			toUpdate.CreateDate = check.CreateDate;
			var result = _requestRespository.Update(toUpdate);

			if (requestDto.Status == Utilities.Enums.StatusLevel.Deleted || requestDto.Status == Utilities.Enums.StatusLevel.Completed || requestDto.Status == Utilities.Enums.StatusLevel.Canceled || requestDto.Status == Utilities.Enums.StatusLevel.Rejected)
			{
				var listFasility = _listFasilityRepository.GetAllListFasilityByReqGuid(requestDto.Guid);
				if (listFasility is null)
				{
					return Ok(new ResponseOKHandler<String>("Updated Data Success"));
				}

				foreach (var data in listFasility)
				{
					var fasility = _fasilityRepository.GetByGuid(data.FasilityGuid);
					fasility.Stock = fasility.Stock + data.TotalFasility;
					_fasilityRepository.Update(fasility);
				}

			}

			return Ok(new ResponseOKHandler<String>("Updated Data Success"));
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

	[HttpPost]
	public IActionResult Create(CreateRequestDto requestDto)
	{
		if(requestDto.RoomGuid.Equals(""))
		{
			requestDto.RoomGuid = null;
		}
		try
		{
			var result = _requestRespository.Create(requestDto);
			return Ok(new ResponseOKHandler<RequestDto>((RequestDto)result));
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
			{
				Code = StatusCodes.Status500InternalServerError,
				Status = HttpStatusCode.InternalServerError.ToString(),
				Message = "Failed to Create data",
				Error = ex.Message
			});
		}
	}

	[HttpDelete("{guid}")]
	public IActionResult Delete(Guid guid)
	{
		try
		{
			var entity = _requestRespository.GetByGuid(guid);
			if (entity is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Data Not Found"
				});
			}
			var result = _requestRespository.Delete(entity);
			return Ok(new ResponseOKHandler<String>("Success to Delete data"));
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
			{
				Code = StatusCodes.Status500InternalServerError,
				Status = HttpStatusCode.InternalServerError.ToString(),
				Message = "Failed to Deleted data",
				Error = ex.Message
			});
		}

	}


	public static int DurationCalculator(DateTime startDate, DateTime endDate)
	{
		int totalDays = (endDate - startDate).Days;
		//int weekendDays = 0;

		/*for (int i = 0; i < totalDays; i++)
		{
			DateTime currentDate = startDate.AddDays(i);
			if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
			{
				weekendDays++;
			}
		}*/
		//int weekDays = totalDays - weekendDays;
		return totalDays;
	}

}
