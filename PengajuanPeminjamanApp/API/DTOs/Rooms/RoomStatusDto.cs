using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Rooms;

public class RoomStatusDto
{
	public Guid Guid { get; set; }
	public string Name { get; set; }
	public int Floor { get; set; }
	public StatusLevel Status { get; set; }

}
