using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace CoWorker.Models.Security.Authentication
{
    public class AdditionalClaimTransformation : IClaimsTransformation
    {
        private readonly IEnumerable<IClaimProvider> _providers;

        public AdditionalClaimTransformation(IEnumerable<IClaimProvider> providers)
        {
            this._providers = providers;
        }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal user)
        {
            var claims = _providers.AsEnumerable()
                .SelectMany(x => x.Create(user));;
            return Task.FromResult(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        }
    }
}
