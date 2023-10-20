using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class RequestFasilityDbContext : DbContext
{
    public RequestFasilityDbContext(DbContextOptions<RequestFasilityDbContext> options) : base(options){}

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Fasility> Fasilities { get; set; }
    public DbSet<ListFasility> ListFasilities { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		
	}

}
