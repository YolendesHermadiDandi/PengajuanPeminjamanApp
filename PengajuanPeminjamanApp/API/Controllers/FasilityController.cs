using API.Contracts;
using API.DTOs.Fasilities;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("API/[controller]")]
public class FasilityController : ControllerBase
{
	private readonly IFasilityRepository _fasilityRepository;
	public FasilityController(IFasilityRepository fasilityRepository)
	{
		_fasilityRepository = fasilityRepository;
	}



	[HttpGet]
	public IActionResult GetAll()
	{
		var result = _fasilityRepository.GetAll();
		if (!result.Any())
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}
		var data = result.Select(x => (FasilityDto)x);
		return Ok(new ResponseOKHandler<IEnumerable<FasilityDto>>(data));
	}


	[HttpGet("{guid}")]
	public IActionResult GetByGuid(Guid guid)
	{
		var result = _fasilityRepository.GetByGuid(guid);
		if (result is null)
		{
			return NotFound(new ResponseErrorHandler
			{
				Code = StatusCodes.Status404NotFound,
				Status = HttpStatusCode.NotFound.ToString(),
				Message = "Data Not Found"
			});
		}

		return Ok(new ResponseOKHandler<FasilityDto>((FasilityDto)result));
	}

	[HttpPut]
	public IActionResult Update(FasilityDto fasilityDto)
	{
		try
		{
			var check = _fasilityRepository.GetByGuid(fasilityDto.Guid);
			if (check is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Data Not Found"
				});
			}
			Fasility toUpdate = (Fasility)fasilityDto;
			toUpdate.CreateDate = check.CreateDate;
			var result = _fasilityRepository.Update(toUpdate);
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
	public IActionResult Create(CreateFasilityDto fasilityDto)
	{
		try
		{
			var result = _fasilityRepository.Create(fasilityDto);
			return Ok(new ResponseOKHandler<FasilityDto>((FasilityDto)result));
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
			var entity = _fasilityRepository.GetByGuid(guid);
			if (entity is null)
			{
				return NotFound(new ResponseErrorHandler
				{
					Code = StatusCodes.Status404NotFound,
					Status = HttpStatusCode.NotFound.ToString(),
					Message = "Data Not Found"
				});
			}
			var result = _fasilityRepository.Delete(entity);
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
