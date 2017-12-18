using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Models.Abstractions.Stores
{

    public static class X509Extensions
    {
        public static IServiceCollection AddCertificate(this IServiceCollection builder, string name, StoreLocation location = StoreLocation.LocalMachine, X509FindType nameType = X509FindType.FindBySubjectDistinguishedName)
            => builder.AddSingleton<IDataProvider<X509Certificate2>>(
                p => new DefaultDataProvider<X509Certificate2>(
                    name,
                    () => FindCertificate(name, location, nameType)
                        ?? throw new InvalidOperationException($"certificate: '{name}' not found in certificate store")
                ));

        public static IServiceCollection AddCertificate(this IServiceCollection services,string filename,string password)
            => services.AddSingleton<IDataProvider<X509Certificate2>>(
                p => new DefaultDataProvider<X509Certificate2>(
                    filename.Split('.').First(), 
                    new X509Certificate2(filename, password)));

        private static X509Certificate2 FindCertificate(string name, StoreLocation location, X509FindType findType)
               => new X509Store(StoreName.My, location).Certificates.Find(findType, name, false).OfType<X509Certificate2>().FirstOrDefault();

    }
}
