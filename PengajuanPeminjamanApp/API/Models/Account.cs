using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_account")]
public class Account : GeneralModel
{

    [Column("password", TypeName = "nvarchar(max)")]
    public string Password { get; set; }

    [Column("otp", TypeName = "int")]
    public int Otp { get; set; }
    [Column("is_used", TypeName = "bit")]
    public bool IsUsed {  get; set; }
    [Column("expired_time", TypeName = "datetime2")]
    public DateTime ExpiredTime { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
