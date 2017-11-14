using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using CoWorker.Primitives;
using System;

namespace IdentitySample.Controllers
{

    public class CertificateStoreBuilder
    {
        private readonly IServiceCollection _services;

        public CertificateStoreBuilder(IServiceCollection services)
        {
            _services = services;
        }
        public IServiceCollection Service { get; }
    }
}
