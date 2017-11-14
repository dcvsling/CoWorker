using System.Collections;
using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.ApplicationParts
{

    public class ApplicationPartsBuilder
    {
        public ApplicationPartsBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
