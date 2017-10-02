using System.Web;
using Microsoft.AspNetCore.Identity;
namespace  CoWorker.Models.Security.Identity
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	public class IdentityDbContext : IdentityDbContext<User,Role,Guid>
	{
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(new DbContextOptionsBuilder(options).Options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .ToTable("User")
                .HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey("Id")
                .HasPrincipalKey(u => u.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>()
                .ToTable("Role")
                .HasMany(r => r.Claims)
                .WithOne()
                .HasForeignKey("Id")
                .HasPrincipalKey(r => r.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");


        }
    }
}
