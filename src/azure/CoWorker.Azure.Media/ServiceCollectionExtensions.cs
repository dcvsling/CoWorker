using CoWorker.Azure.Media.Factory;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CoWorker.Azure.Media.Repository;
using CoWorker.Azure.Media.Internal;

namespace MediaService.Core
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureMediaService(this IServiceCollection services)
            => services
                .AddSingleton<IConfigureOptions<AMSOptions>, DefaultAMSConfigureOptions>()
                .AddSingleton<IAzureMediaServiceFactory, AzureMediaServiceFactory>()
                .AddSingleton(p => p.GetService<IAzureMediaServiceFactory>().Create())
                .DestructureService<CloudMediaContext>()
                .AddScoped<IRepository<IChannel>, ChannelRepository>()
                .AddScoped<IRepository<IAsset>, AssetRepository>()
                .AddScoped<IRepository<IProgram>, ProgramRepository>()
                .AddScoped<IRepository<IStreamingEndpoint>, StreamingEndPointRepository>()
                .AddSingleton(p => p.GetService<CloudMediaContext>().Channels)
                .AddSingleton(p => p.GetService<CloudMediaContext>().Assets)
                .AddSingleton(p => p.GetService<CloudMediaContext>().Programs)
                .AddSingleton(p => p.GetService<CloudMediaContext>().StreamingEndpoints)
                .AddSingleton<IChannelFactory, ChannelFactory>()
                .AddSingleton<IConfigureOptions<ChannelCreationOptions>, ChannelMemberConfigureOptions>()
                .AddSingleton<IChannelConfigureOptions, DefaultChannelMemberOptions>();

        public static IServiceCollection DestructureService<TService>(this IServiceCollection services)
            where TService : class
            => new ServiceDestructure(services).Destructure<TService>();
        
        public static IRepository<IProgram> With(this ProgramRepository repo, string channelName)
            => new ProgramRepository(repo, channelName);
    }
}
