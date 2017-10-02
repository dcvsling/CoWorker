
namespace  CoWorker.Models.Security.Identity
{
	using System;

	using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser<Guid>
    {
        public List<Claim> Claims { get; set; }
    }

    public class UserClaim : IdentityUserClaim<Guid> { }
    public class UserLogin : IdentityUserLogin<Guid> { }
    public class UserRole : IdentityUserRole<Guid> { }

    public class Login
    {
        public virtual String LoginProvider { get; set; }
        public virtual String ProviderKey { get; set; }
        public virtual String ProviderDisplayName { get; set; }
    }

    public class Claim
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
