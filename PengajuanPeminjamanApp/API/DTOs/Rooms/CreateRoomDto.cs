using API.Models;

namespace API.DTOs.Rooms;

public class CreateRoomDto
{
    public string Name { get; set; }
    public int Floor { get; set; }

    public static implicit operator Room(CreateRoomDto roomDto)
    {
        return new Room
        {
            Name = roomDto.Name,
            Floor = roomDto.Floor,
            CreateDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
