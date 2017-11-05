using System.Security.Claims;
using System.Collections.Generic;

namespace CoWorker.Models.Security.Authentication
{
    public class DefaultClaimProvider : IClaimProvider
    {
        public IEnumerable<Claim> Create(ClaimsPrincipal user)
            => user.Claims;
    }
}
