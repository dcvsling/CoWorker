using System.Security.Cryptography.X509Certificates;

namespace IdentitySample.Controllers
{

    public interface ICertificateStore
    {
        X509Certificate Get(string name);
    }
}
