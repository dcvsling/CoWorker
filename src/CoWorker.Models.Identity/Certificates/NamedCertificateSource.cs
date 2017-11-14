using System.Security.Cryptography.X509Certificates;
using CoWorker.Primitives;
using System;

namespace IdentitySample.Controllers
{

    public class NamedCertificateSource : IName, ICertificateSource
    {
        public string Name { get; set; }
        public Func<X509Certificate> Certificate { get; set; }
    }
}
