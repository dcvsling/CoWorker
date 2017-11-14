using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using CoWorker.Primitives;
using System;

namespace IdentitySample.Controllers
{

    public static class CertificateExtensions
    {
        public static CertificateStoreBuilder AddCertificateStore(this IServiceCollection services)
        {
            services.TryAddSingleton<ICertificateStore, CertificateStore>();
            services.TryAddSingleton<ICertificateSource, NamedCertificateSource>();
            return new CertificateStoreBuilder(services);
        }

        public static CertificateStoreBuilder Add(this CertificateStoreBuilder builder, string name, Func<X509Certificate2> certificate)
        {
            builder.Service
                .Configure<NamedCertificateSource>(x =>
                {
                    x.Name = name;
                    x.Certificate = certificate;
                });
            return builder;
        }
    }
}
