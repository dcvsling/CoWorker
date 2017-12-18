using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using CoWorker.Primitives;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace CoWorker.Models.Mvc
{

    public class CustomMvcCoreBuilder : IMvcCoreBuilder
    {
        private readonly IMvcCoreBuilder _builder;

        public CustomMvcCoreBuilder(IMvcCoreBuilder builder)// : base(builder.Services,builder.PartManager)
        {
            _builder = builder;
        }

        public IServiceCollection Services => _builder.Services;

        public ApplicationPartManager PartManager => _builder.PartManager;

        public IMvcCoreBuilder Configure()
        {
            _builder.Services.AddSingleton<IDesignTimeMvcCoreBuilderConfiguration, MvcDesignTimeBuilder>()
                  .BuildServiceProvider()
                  .GetService<IDesignTimeMvcCoreBuilderConfiguration>()
                  .ConfigureMvc(_builder);
            return _builder;
        }
    }
}
