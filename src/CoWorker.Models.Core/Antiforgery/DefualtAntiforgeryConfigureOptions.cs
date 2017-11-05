namespace CoWorker.Models.Core.Antiforgery
{
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Defines the <see cref="DefaultAntiforgeryConfigureOptions" />
    /// </summary>
    public class DefaultAntiforgeryConfigureOptions : IConfigureOptions<AntiforgeryOptions>
    {
        /// <summary>
        /// Defines the COOKIE_NAME
        /// </summary>
        private const string COOKIE_NAME = "CSRF-TOKEN";

        /// <summary>
        /// Defines the NON_COOKIE_NAME
        /// </summary>
        private const string NON_COOKIE_NAME = "X-CSRF-TOKEN";

        /// <summary>
        /// Defines the COOKIE_PATH
        /// </summary>
        private const string COOKIE_PATH = "/SECURITY-TOKEN";

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="options">The <see cref="AntiforgeryOptions"/></param>
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
