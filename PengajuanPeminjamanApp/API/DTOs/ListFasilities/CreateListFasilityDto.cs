using API.Models;

namespace API.DTOs.ListFasilities;

public class CreateListFasilityDto
{
    public Guid RequestGuid { get; set; }
    public Guid FasilityGuid { get; set; }
    public int TotalFasility { get; set; }

    public static implicit operator ListFasility(CreateListFasilityDto createListFasilityDto)
    {
        return new ListFasility
        {
            RequestGuid = createListFasilityDto.RequestGuid,
            FasilityGuid = createListFasilityDto.FasilityGuid,
            TotalFasility = createListFasilityDto.TotalFasility,
            CreateDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
