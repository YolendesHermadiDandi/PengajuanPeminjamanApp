using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class RequestFasilityDbContext : DbContext
{
    public RequestFasilityDbContext(DbContextOptions<RequestFasilityDbContext> options) : base(options) { }

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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.Nik
        }).IsUnique();

        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.Email
        }).IsUnique();

        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.PhoneNumber
        }).IsUnique();


        modelBuilder.Entity<AccountRole>()
                    .HasOne(r => r.Role)
                    .WithMany(ar => ar.AccountRoles)
                    .HasForeignKey(ar => ar.RoleGuid)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Account>()
                    .HasMany(ar => ar.AccountRoles)
                    .WithOne(a => a.Account)
                    .HasForeignKey(ar => ar.AccountGuid)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
                    .HasOne(a => a.Account)
                    .WithMany(n => n.Notifications)
                    .HasForeignKey(n => n.AccountGuid)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
                    .HasOne(a => a.Account)
                    .WithOne(a => a.Employee)
                    .HasForeignKey<Account>(a => a.Guid);

        modelBuilder.Entity<Request>()
                    .HasOne(e => e.Employee)
                    .WithMany(r => r.Request)
                    .HasForeignKey(r => r.EmployeeGuid)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
                    .HasOne(r => r.Room)
                    .WithOne(rq => rq.Request)
                    .HasForeignKey<Request>(rq => rq.RoomGuid);

        modelBuilder.Entity<ListFasility>()
                    .HasOne(r => r.Request)
                    .WithMany(l => l.ListFasilities)
                    .HasForeignKey(l => l.RequestGuid)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ListFasility>()
                    .HasOne(f => f.Fasility)
                    .WithMany(l => l.ListFasilities)
                    .HasForeignKey(l => l.FasilityGuid)
                    .OnDelete(DeleteBehavior.Restrict);
    }

}
