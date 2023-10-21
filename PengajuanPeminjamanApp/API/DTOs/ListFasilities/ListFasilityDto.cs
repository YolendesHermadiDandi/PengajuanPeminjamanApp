using API.DTOs.Rooms;
using API.Models;

namespace API.DTOs.ListFasilities;

public class ListFasilityDto
{
	public Guid Guid {  get; set; }
	public Guid RequestGuid { get; set; }
	public Guid FasilityGuid { get; set; }
	public int TotalFasility { get; set; }

	public static implicit operator ListFasilityDto(ListFasility listFasility)
	{
		return new ListFasilityDto
		{
			Guid = listFasility.Guid,
			RequestGuid = listFasility.RequestGuid,
			FasilityGuid = listFasility.FasilityGuid,
			TotalFasility = listFasility.TotalFasility
		};
	}

	public static explicit operator ListFasility(ListFasilityDto listFasilitiesDto)
	{
		return new ListFasility
		{
			Guid = listFasilitiesDto.Guid,
			RequestGuid = listFasilitiesDto.RequestGuid,
			FasilityGuid = listFasilitiesDto.FasilityGuid,
			TotalFasility = listFasilitiesDto.TotalFasility,
			ModifiedDate = DateTime.Now
		};
	}
}
