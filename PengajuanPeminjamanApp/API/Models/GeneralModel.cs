using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public abstract class GeneralModel
{
    [Key, Column("guid")]
    public Guid Guid { get; set; }
    [Column("create_date")]
    public DateTime CreateDate { get; set; }
    [Column("modified_date")]
    public DateTime ModifiedeDate { get; set; }
}
