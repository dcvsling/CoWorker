using CoWorker.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace CoWorker.LightMvc.Swagger
{

    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        private readonly IOptions<AuthorizationOptions> authorizationOptions;

        public SecurityRequirementsOperationFilter(IOptions<AuthorizationOptions> authorizationOptions)
        {
            this.authorizationOptions = authorizationOptions;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            context.ApiDescription.ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .Select(x => x is AuthorizeAttribute attr ? attr : default)
                .Where(x => x != null)
                .Distinct()
                .Each(x =>
                {
                    operation.Responses.TryAdd("401", new Response { Description = "Unauthorized" });
                    operation.Responses.TryAdd("403", new Response { Description = "Forbidden" });
                    operation.Security = new List<IDictionary<string, IEnumerable<string>>>()
                    {
                        new Dictionary<string,IEnumerable<string>>()
                        {
                            [nameof(x.Policy)] = new string[]{ x.Policy }
                        },
                        new Dictionary<string,IEnumerable<string>>()
                        {
                            [nameof(x.AuthenticationSchemes)] = new string[]{ x.AuthenticationSchemes }
                        },
                        new Dictionary<string,IEnumerable<string>>()
                        {
                            [nameof(x.Roles)] = new string[]{ x.Roles }
                        }
                    };
                });
        }
    }
}
