using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CoWorker.Models.Security.Authentication
{

    public interface IClaimProvider
    {
        IEnumerable<Claim> Create(ClaimsPrincipal user);
    }

    public class DefaultClaimProvider : IClaimProvider
    {
        public IEnumerable<Claim> Create(ClaimsPrincipal user)
            => user.Claims;
    }
}
