using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tr_m_request")]
public class Request : GeneralModel
{
	[Column("room_guid")]
	public Guid RoomGuid { get; set; }
	[Column("employee_guid")]
	public Guid EmployeeGuid { get; set; }
	[Column("status")]
	public StatusLevel Status { get; set; }
	[Column("start_date")]
	public DateTime StartDate { get; set; }
	[Column("end_date")]
	public DateTime EndDate{ get; set; }

	public Room? Room { get; set; }
	public Employee? Employee { get; set; }
	public ICollection<ListFasility> ListFasilities { get; set;}

}
