using API.DTOs.Rooms;
using API.Utilities.Enums;

namespace API.DTOs.Requests;

public class ListRequestDto
{
    public Guid Guid { get; set; }
    public RoomDto rooms { get; set; }
    public Guid EmployeeGuid { get; set; }
    public StatusLevel Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
