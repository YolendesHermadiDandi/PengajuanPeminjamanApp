using API.Models;

namespace API.DTOs.Fasilities;

public class FasilityDto
{
	public Guid Guid { get; set; }
	public string Name { get; set; }
	public int Stock { get; set; }

	public static implicit operator FasilityDto(Fasility fasility)
	{
		return new FasilityDto
		{
			Guid = fasility.Guid,
			Name = fasility.Name,
			Stock = fasility.Stock
		};
	}

	public static explicit operator Fasility(FasilityDto fasility)
	{
		return new Fasility
		{
			Guid = fasility.Guid,
			Name = fasility.Name,
			Stock = fasility.Stock,
			ModifiedDate = DateTime.Now
		};
	}
}
