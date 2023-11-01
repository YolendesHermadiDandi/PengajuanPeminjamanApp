namespace API.DTOs.Requests;

public class StatusRequestRoomDto
{
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public Guid roomGuid { get; set; }
}
