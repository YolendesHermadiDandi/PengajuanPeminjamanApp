using API.Models;

namespace API.DTOs.Rooms;

public class RoomDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public int Floor { get; set; }

    public static implicit operator RoomDto(Room room)
    {
        return new RoomDto
        {
            Guid = room.Guid,
            Name = room.Name,
            Floor = room.Floor
        };
    }

    public static explicit operator Room(RoomDto room)
    {
        return new Room
        {
            Guid = room.Guid,
            Name = room.Name,
            Floor = room.Floor,
            ModifiedDate = DateTime.Now
        };
    }
}
