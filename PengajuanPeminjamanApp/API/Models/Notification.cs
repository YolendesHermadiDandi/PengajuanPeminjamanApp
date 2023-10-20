using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_notification")]
public class Notification : GeneralModel
{
    [Column("account_guid")]
    public Guid AccountGuid { get; set; }
    [Column("massage", TypeName = "nvarchar(100)")]
    public Guid Massage { get; set; }
    [Column("is_seen")]
    public Boolean IsSeen { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    public Account? Account { get; set; }
}
