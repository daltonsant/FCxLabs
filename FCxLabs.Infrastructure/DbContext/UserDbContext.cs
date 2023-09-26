using FCxLabs.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FCxLabs.Infrastructure.DbContext;

public class UserDbContext : IdentityDbContext<User> 
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
			
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        AddRoles(builder);
        AddConstraints(builder);
    }
	
    private void AddConstraints(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.Cpf)
            .IsUnique();
			
        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();			
    }
    private void AddRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }	
        );
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cto = default)
    {
        foreach(var entry in ChangeTracker.Entries<User>())
        {
            switch(entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedOn = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cto);
    }
}
