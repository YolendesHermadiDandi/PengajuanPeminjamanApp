using API.Contracts;
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
public class RequestController : ControllerBase
{
	private readonly IRequestRepository _requestRespository;

	public RequestController(IRequestRepository requestRespository)
	{
		_requestRespository = requestRespository;
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
}
