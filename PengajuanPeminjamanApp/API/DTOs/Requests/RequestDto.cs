using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Requests;

public class RequestDto
{
	public Guid Guid { get; set; }
    public Guid? RoomGuid { get; set; }
	public Guid EmployeeGuid { get; set; }
	public StatusLevel Status { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }

	public static implicit operator RequestDto(Request request)
	{
		return new RequestDto
		{
			Guid = request.Guid,
			RoomGuid = request.RoomGuid.Equals("") ? null : request.RoomGuid,
			EmployeeGuid = request.EmployeeGuid,
			Status = request.Status,
			StartDate = request.StartDate,
			EndDate = request.EndDate,
		};
	}

	public static explicit operator Request(RequestDto requestDto)
	{
		return new Request
		{
			Guid = requestDto.Guid,
			RoomGuid = requestDto.RoomGuid,
			EmployeeGuid = requestDto.EmployeeGuid,
			Status = requestDto.Status,
			StartDate = requestDto.StartDate,
			EndDate = requestDto.EndDate,
			ModifiedDate = DateTime.Now
		};
	}
}
