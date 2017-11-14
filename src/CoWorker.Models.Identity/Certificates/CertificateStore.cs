using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System;

namespace IdentitySample.Controllers
{

    public class CertificateStore : ICertificateStore
    {
        private readonly IDictionary<string,Func<X509Certificate>> _sources;

        public CertificateStore(IEnumerable<ICertificateSource> sources)
        {
            _sources = sources.ToDictionary(x => x.Name,x => x.Certificate);
        }

        public X509Certificate Get(string name)
            => new X509Certificate(_sources.FirstOrDefault(x => x.Key == name).Value());


    }
}
