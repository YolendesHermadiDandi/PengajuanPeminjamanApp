using API.Contracts;
using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
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

	public ListFasilityController(IListFasilityRepository listFasilityRepository)
	{
		_listFasilityRepository = listFasilityRepository;
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
			ListFasility toUpdate = (ListFasility)listFasilityDto;
			toUpdate.CreateDate = check.CreateDate;
			var result = _listFasilityRepository.Update(toUpdate);
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
	public IActionResult Create(CreateListFasilityDto listFasilityDto)
	{
		try
		{
			var result = _listFasilityRepository.Create(listFasilityDto);
			return Ok(new ResponseOKHandler<ListFasilityDto>((ListFasilityDto)result));
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
