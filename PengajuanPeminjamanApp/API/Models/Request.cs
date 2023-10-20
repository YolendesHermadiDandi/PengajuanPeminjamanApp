using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tr_m_request")]
public class Request
{
	[Column("room_guid")]
	public Guid RoomGuid { get; set; }
	[Column("employee_guid")]
	public Guid EmployeeGuid { get; set; }
	[Column("status")]
	public int status { get; set; }
	[Column("start_date")]
	public DateTime StartDate { get; set; }
	[Column("end_date")]
	public DateTime endDate{ get; set; }
}
