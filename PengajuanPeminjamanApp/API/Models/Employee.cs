using API.Utilities.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_employee")]
public class Employee : GeneralModel
{
    [Column("first_name", TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }
    [Column("last_name", TypeName = "nvarchar(50)")]
    public string? LastName { get; set; }
    [Column("email", TypeName = "nvarchar(50)")]
    public string Email { get; set; }
    [Column("nik", TypeName = "nchar(10)")]
    public string Nik { get; set; }
    [Column("phone_number", TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; }
    [Column("gender", TypeName = "int")]
    public GenderLevel Gender { get; set; }

    public Account? Account { get; set; }
    public ICollection<Request> Request { get; set; }
}
