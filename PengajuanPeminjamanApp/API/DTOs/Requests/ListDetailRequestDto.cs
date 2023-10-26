using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
using API.DTOs.Rooms;
using API.Utilities.Enums;

namespace API.DTOs.Requests;

public class ListDetailRequestDto
{
    public Guid Guid { get; set; }
    public RoomDto rooms { get; set; }
    public IEnumerable<ListDetailFasilityDto> fasility { get; set; }
    public Guid EmployeeGuid { get; set; }
    public StatusLevel Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
