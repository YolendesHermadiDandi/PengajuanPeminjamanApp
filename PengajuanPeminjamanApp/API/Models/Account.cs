using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_account")]
public class Account : GeneralModel
{

    [Column("password", TypeName = "nvarchar(25)")]
    public string Password { get; set; }
    [Column("otp", TypeName = "int")]
    public int Otp { get; set; }

    public Employee? Employee { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
