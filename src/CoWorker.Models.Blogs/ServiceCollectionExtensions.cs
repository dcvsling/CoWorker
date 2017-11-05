namespace CoWorker.Models.Blogs
{
    using Microsoft.Extensions.Options;
    using CoWorker.Models.Blogs.Migrations;
    using CoWorker.Models.Blog;
    using Microsoft.Extensions.DependencyInjection;
    using CoWorker.Models.Blog.ValueGenerators;
    using Microsoft.Extensions.Internal;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlog(this IServiceCollection services)
            => services
                .AddSingleton<BlogSeed>()
                .AddDefaultValueGeneratorProviders()
                .AddSingleton<IConfigureOptions<PostRelated>,PostRelatedConfigureOptions>()
                .AddScoped<IBlogFactory, BlogFactory>()
                .AddScoped<BlogController>();

        public static IServiceCollection AddDefaultValueGeneratorProviders(this IServiceCollection services)
        {
            services
                  .AddSingleton<DatetimeOffsetValueGenerator>()
                  .AddSingleton<DatetimeValueGenerator>()
                  .AddSingleton<CurrentUserValueGenerator>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
            return services;
        }
    }

    public static class BlogDefault
    {
        public static readonly DateTime MAX_DATE = new DateTime(2100, 12, 31);
        public static readonly DateTime MIN_DATE = new DateTime(1900, 1, 1);
    }
}
