using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_role")]
public class Role : GeneralModel
{
    [Column("name", TypeName = "nvarchar(25)")]
    public string Name { get; set; }

    public ICollection<AccountRole>? AccountRoles { get; set; }
}
