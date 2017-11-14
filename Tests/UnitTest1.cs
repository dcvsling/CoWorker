using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Tests
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            new UnitTest1().CreateHost().Run();
        }
    }
    public class UnitTest1
    {


        public IWebHost CreateHost()
            => WebHost.CreateDefaultBuilder().ConfigureServices(
                srv => srv.AddDataProtection().Services
                    .AddSingleton<IPostConfigureOptions<OAuthOptions>, OAuthPostConfigureOptions<OAuthOptions, OAuthHandler<OAuthOptions>>>()
                    .AddAuthentication()
                    .AddRemoteScheme<OAuthOptions, OAuthHandler<OAuthOptions>>("mytest", "mytest", o =>
                     {
                         o.ClientId = "00bd2239-78ed-4791-afd4-5316e92af615";
                         o.ClientSecret = "frp5eQYxWq49HQKZsyOYYY3k3VsEkrq71UcB4l1dl4g=";
                         o.AuthorizationEndpoint = "https://login.microsoftonline.com/55604d97-faf8-4a3c-8f8d-c7a4fbc9b8b6/oauth2/authorize";
                         o.TokenEndpoint = "https://login.microsoftonline.com/55604d97-faf8-4a3c-8f8d-c7a4fbc9b8b6/oauth2/token";
                         o.CallbackPath = "/";
                     })).Configure(
                        app => app.UseAuthentication().Map(
                            "/auth",
                            authapp => authapp.Use(
                            next => ctx => ctx.RequestServices.GetService<IAuthenticationService>().ChallengeAsync(ctx, "mytest", new AuthenticationProperties() { RedirectUri = "/" })))
                        ).Build();

    }

}
