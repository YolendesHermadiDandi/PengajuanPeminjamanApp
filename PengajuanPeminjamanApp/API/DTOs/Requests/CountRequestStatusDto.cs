using API.Utilities.Enums;

namespace API.DTOs.Requests;

public class CountRequestStatusDto
{
    public StatusLevel status {  get; set; }
    public int count { get; set; }
}
