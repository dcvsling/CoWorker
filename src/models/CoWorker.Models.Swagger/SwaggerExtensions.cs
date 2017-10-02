using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CoWorker.LightMvc.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services
                .AddSwaggerGen(o =>
                {
                    o.SwaggerDoc("v1", new Info { Title = "My API V1", Version = "v1" });
                    o.SwaggerDoc("v2", new Info { Title = "My API V2", Version = "v2" });
                    o.OperationFilter<SecurityRequirementsOperationFilter>();
                    o.DescribeAllParametersInCamelCase();
                });

        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
            => app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/swagger/{documentName}.json";
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
                })
                .UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swg";
                    c.SwaggerEndpoint("/api/swagger/v1.json", "My API V1");
                    c.EnabledValidator();
                    c.BooleanValues(new object[] { 0, 1 });
                    c.DocExpansion("full");
                    c.SupportedSubmitMethods(new[] { "get", "post", "put", "delete", "patch" });
                    c.ShowRequestHeaders();
                    c.ShowJsonEditor();
                });
    }
}
