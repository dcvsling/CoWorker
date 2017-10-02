using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
namespace CoWorker.EntityFramework.ValueGenerators
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserValueGenerator : ValueGenerator<string>
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserValueGenerator(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public override Boolean GeneratesTemporaryValues => false;

        public override String Next(EntityEntry entry)
        {
            var user = _accessor.HttpContext.User;
            if (user.Identity.IsAuthenticated) throw new UnauthorizedAccessException();
            return $"{user.FindFirst(ClaimTypes.NameIdentifier).Value}#{user.FindFirst(ClaimTypes.Email).Value}";
        }
    }
}
