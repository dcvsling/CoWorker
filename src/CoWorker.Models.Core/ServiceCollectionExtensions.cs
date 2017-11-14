namespace CoWorker.Builder
{
    using Microsoft.AspNetCore.Hosting;

    public static class HttpsExtensions
    {
        public static IWebHostBuilder AddHostingStartup<THostingStartup>(this IWebHostBuilder builder) where THostingStartup : IHostingStartup, new()
        {
            new THostingStartup().Configure(builder);
            return builder;
        }
    }
}