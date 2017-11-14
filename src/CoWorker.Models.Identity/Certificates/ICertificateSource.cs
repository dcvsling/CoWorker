using CoWorker.Primitives;
using System;
using System.Security.Cryptography.X509Certificates;

namespace IdentitySample.Controllers
{
    public interface ICertificateSource :IName
    {
        Func<X509Certificate> Certificate { get; }
    }
}