using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Model;

public abstract class GeneralModel
{
    [Key, Column("guid")]
    public Guid Guid { get; set; }
    [Column("create_date")]
    public DateTime CreateDate { get; set; }
    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }
}
