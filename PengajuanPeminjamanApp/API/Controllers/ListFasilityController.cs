using API.Contracts;
using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
using API.DTOs.Requests;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("API/[controller]")]
public class ListFasilityController : ControllerBase
{
	private readonly IListFasilityRepository _listFasilityRepository;
	private readonly IFasilityRepository _fasilityRepository;
	private readonly IRequestRepository _requestRepository;

	public ListFasilityController(IListFasilityRepository listFasilityRepository, IFasilityRepository fasilityRepository, IRequestRepository requestRepository)
	{
		_listFasilityRepository = listFasilityRepository;
		_fasilityRepository = fasilityRepository;
		_requestRepository = requestRepository;
	}

	[HttpGet]
	public IActionResult GetAll()
	{
		var result = _listFasilityRepository.GetAll();
		if (!result.Any())
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}
		var data = result.Select(x => (ListFasilityDto)x);
		return Ok(new ResponseOKHandler<IEnumerable<ListFasilityDto>>(data));
	}


	[HttpGet("{guid}")]
	public IActionResult GetByGuid(Guid guid)
	{
		var result = _listFasilityRepository.GetByGuid(guid);
		if (result is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		return Ok(new ResponseOKHandler<ListFasilityDto>((ListFasilityDto)result));
	}
	
	[HttpGet("GetListFasilityByRequestGuid")]
	public IActionResult GetListFasilityByRequestGuid(Guid requestGuid)
	{
        var listFasility = _listFasilityRepository.GetAllListFasilityByReqGuid(requestGuid);
		var fasility = _fasilityRepository.GetAll();
        if (listFasility is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		var data = from fas in fasility
				   join li in listFasility on fas.Guid equals li.FasilityGuid
				   where li.RequestGuid == requestGuid
				   select new
				   {
					   FasilityGuid = fas.Guid,
					   Name = fas.Name,
					   TotalFasility = li.TotalFasility
				   };
        return Ok(new ResponseOKHandler<IEnumerable<object>>(data));
	}

	[HttpPut]
	public IActionResult Update(ListFasilityDto listFasilityDto)
	{
		try
		{
			var check = _listFasilityRepository.GetByGuid(listFasilityDto.Guid);
			if (check is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Data Not Found"
				});
			}
			var request = _requestRepository.GetByGuid(listFasilityDto.RequestGuid);
			if (request.Status != 0)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status400BadRequest,
					Status = HttpStatusCode.BadRequest.ToString(),
					Message = "Sorry You Cant Change Fasility Anymore"
				});
			}

			var fasility = _fasilityRepository.GetByGuid(listFasilityDto.FasilityGuid);
			if (fasility is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Fasility Not Found"
				});
			}
			fasility.Stock = fasility.Stock + check.TotalFasility;
			if ((fasility.Stock - listFasilityDto.TotalFasility) < 1)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status400BadRequest,
					Status = HttpStatusCode.BadRequest.ToString(),
					Message = "Fasility Not Enough"
				});
			}
			fasility.Stock = fasility.Stock - listFasilityDto.TotalFasility;
			_fasilityRepository.Update(fasility);

			ListFasility toUpdate = (ListFasility)listFasilityDto;
			toUpdate.CreateDate = check.CreateDate;
			var result = _listFasilityRepository.Update(toUpdate);
			return Ok(new ResponseOKHandler<String>("Updated Fasility Success"));
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
	public IActionResult Create(CreateListFasilityDto listFasilityDto)
	{
		try
		{
			var check = _fasilityRepository.GetByGuid(listFasilityDto.FasilityGuid);
			if (check is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Fasility Not Found"
				});
			}

			if((check.Stock - listFasilityDto.TotalFasility) < 1)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status400BadRequest,
					Status = HttpStatusCode.BadRequest.ToString(),
					Message = "Fasility Not Enough"
				});
			}
			check.Stock = check.Stock - listFasilityDto.TotalFasility;
			_fasilityRepository.Update(check);
			var result = _listFasilityRepository.Create(listFasilityDto);

			return Ok(new ResponseOKHandler<ListFasilityDto>((ListFasilityDto)result));
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
			{
				Code = StatusCodes.Status500InternalServerError,
				Status = HttpStatusCode.InternalServerError.ToString(),
				Message = "Failed to Request Fasility",
				Error = ex.Message
			});
		}
	}

	[HttpDelete("{guid}")]
	public IActionResult Delete(Guid guid)
	{
		try
		{
			var entity = _listFasilityRepository.GetByGuid(guid);
			if (entity is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Data Not Found"
				});
			}
			var result = _listFasilityRepository.Delete(entity);
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
}
