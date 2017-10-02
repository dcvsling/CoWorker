using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;


namespace CoWorker.Net.Antiforgery
{
    using Microsoft.Extensions.Options;
    public class DefaultAntiforgeryConfigureOptions : IConfigureOptions<AntiforgeryOptions>
    {
        private const string COOKIE_NAME = "CSRF-TOKEN";
        private const string NON_COOKIE_NAME = "X-CSRF-TOKEN";
        private const string COOKIE_PATH = "/SECURITY-TOKEN";
        public void Configure(AntiforgeryOptions options)
        {
            options.Cookie.Name = COOKIE_NAME;
            options.FormFieldName = NON_COOKIE_NAME;
            options.HeaderName = NON_COOKIE_NAME;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.Path = COOKIE_PATH;
            options.Cookie.SameSite = SameSiteMode.Strict;
        }
    }
}