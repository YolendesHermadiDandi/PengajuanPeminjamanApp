using API.Data;
using API.Model;

namespace API.Repositories;

public class RoleRepositroy : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepositroy(RequestFasilityDbContext context) : base(context)
	{
        
    }
}
