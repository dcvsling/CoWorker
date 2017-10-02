
namespace  CoWorker.Models.Identity
{
	using System;
	using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Security.Claims;

    public class Role : IdentityRole<Guid>
	{
        public List<Claim> Claims { get; set; }
	}
}
