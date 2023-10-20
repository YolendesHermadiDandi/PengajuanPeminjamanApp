using API.Contracts;
using API.Data;
using API.Model;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
	public EmployeeRepository(RequestFasilityDbContext context) : base(context)
	{

	}
}
