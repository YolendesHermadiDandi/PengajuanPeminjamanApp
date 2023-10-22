using API.Utilities.Enums;

namespace API.DTOs.Requests;

public class DurationRequestDto
{
	public Guid Guid { get; set; }
	public Guid? RoomGuid { get; set; }
	public Guid EmployeeGuid { get; set; }
	public StatusLevel Status { get; set; }
	public int RequestDuration { get; set; }

	
}
