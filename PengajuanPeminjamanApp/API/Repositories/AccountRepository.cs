using API.Contracts;
using API.Data;
using API.Model;

namespace API.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    public AccountRepository(RequestFasilityDbContext context) : base(context)
	{    
    }
}
