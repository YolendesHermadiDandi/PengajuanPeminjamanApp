using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_role")]
public class Role
{
	[Column("name", TypeName ="nvarchar(25)")]
	public string Name { get; set; }
}
