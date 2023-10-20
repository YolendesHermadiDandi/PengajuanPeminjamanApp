using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_list_fasility")]
public class ListFasility : GeneralModel
{
	[Column("request_guid")]
	public Guid RequestGuid { get; set; }
	[Column("fasility_guid")]
	public string FasilityGuid { get; set;}
	[Column("total_fasility")]
	public int TotalFasility { get; set;}

	public Request? Request { get; set; }
	public Fasility? Fasility { get; set;}
}
