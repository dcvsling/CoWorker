using System.Web;
using Microsoft.AspNetCore.Identity;
namespace  CoWorker.Models.Identity
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	public class UserDbContext : IdentityUserContext<User,Guid>
	{
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(new DbContextOptionsBuilder(options).Options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserToken>().ToTable("UserTokens");
        }
    }
}
