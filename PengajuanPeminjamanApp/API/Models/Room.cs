using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_rooms")]
public class Room : GeneralModel
{
	[Column("name", TypeName = "nvarchar(50)")]
	public string Name { get; set; }
	[Column("floor")]
	public int Floor { get; set; }

	public Request? Request { get; set; }
}
