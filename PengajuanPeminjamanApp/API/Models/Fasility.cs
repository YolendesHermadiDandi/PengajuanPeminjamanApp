using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_fasility")]
public class Fasility
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    [Column("stock", TypeName = "int")]
    public int Stock { get; set; }
}
