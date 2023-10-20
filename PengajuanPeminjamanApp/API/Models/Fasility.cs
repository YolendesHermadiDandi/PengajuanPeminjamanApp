using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_fasility")]
public class Fasility : GeneralModel
{
	[Column("name", TypeName = "nvarchar(50)")]
	public string Name { get; set; }
	[Column("stock", TypeName = "int")]
	public int Stock { get; set; }

	public ICollection<ListFasility> ListFasilities { get; set; }
}
