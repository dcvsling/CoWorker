using System.Web;
using Microsoft.AspNetCore.Identity;
namespace  CoWorker.Models.Identity
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class IdentityDbContext : IdentityDbContext<User, Role, Guid,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(new DbContextOptionsBuilder(options).Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
        }
    }
}
