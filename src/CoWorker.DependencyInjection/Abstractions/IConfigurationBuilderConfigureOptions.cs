namespace CoWorker.DependencyInjection.Abstractions
{
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Configuration;

    public interface IConfigurationBuilderConfigureOptions : IConfigureOptions<IConfigurationBuilder> { }
}