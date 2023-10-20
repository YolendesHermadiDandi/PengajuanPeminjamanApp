using API.Data;
using API.Model;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(RequestFasilityDbContext context) : base(context)
    {
        
    }
}
