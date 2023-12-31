﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_rooms")]
public class Room : GeneralModel
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    [Column("floor")]
    public int Floor { get; set; }

    public ICollection<Request>? Request { get; set; }
}
