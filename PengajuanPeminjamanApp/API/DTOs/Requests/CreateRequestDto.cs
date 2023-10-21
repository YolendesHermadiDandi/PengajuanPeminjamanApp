using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Requests;

public class CreateRequestDto
{
	public Guid RoomGuid { get; set; }
	public Guid EmployeeGuid { get; set; }
	public StatusLevel Status { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }

	public static implicit operator Request(CreateRequestDto requestDto)
	{
		return new Request
		{
			RoomGuid = requestDto.RoomGuid,
			EmployeeGuid = requestDto.EmployeeGuid,
			Status = requestDto.Status,
			StartDate = requestDto.StartDate,
			EndDate = requestDto.EndDate,
			CreateDate = DateTime.Now,
			ModifiedDate = DateTime.Now
		};
	}
}
