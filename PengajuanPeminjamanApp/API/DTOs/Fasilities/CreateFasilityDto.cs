using API.Model;

namespace API.DTOs.Fasilities;

public class CreateFasilityDto
{
	public string Name { get; set; }
	public int Stock { get; set; }

	public static implicit operator Fasility(CreateFasilityDto createFasilityDto)
	{
		return new Fasility
		{
			Name = createFasilityDto.Name,
			Stock = createFasilityDto.Stock,
			CreateDate = DateTime.Now,
			ModifiedDate = DateTime.Now
		};
	}
}
