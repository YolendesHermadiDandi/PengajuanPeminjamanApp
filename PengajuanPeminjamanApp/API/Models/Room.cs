using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_rooms")]
public class Room
{
	[Column("name", TypeName = "nvarchar(50)")]
	public string Name { get; set; }
	[Column("floor")]
	public int Floor { get; set; }
	[Column("status")]
	public int Status { get; set; }

}
