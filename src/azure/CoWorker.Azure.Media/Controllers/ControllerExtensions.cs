using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Azure.Media.Controllers
{

    public static class ControllerExtensions
    {
        public static IServiceCollection AddAMSController(this IServiceCollection services)
            => services.AddScoped<AssetController>()
                .AddScoped<ChannelController>()
                .AddScoped<ProgramController>();
    }
}
