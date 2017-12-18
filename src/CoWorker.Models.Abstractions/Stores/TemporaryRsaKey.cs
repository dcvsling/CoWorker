using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace CoWorker.Models.Abstractions.Stores
{
    internal class TemporaryRsaKey
    {
        internal static TemporaryRsaKey Empty => new TemporaryRsaKey();

        private string filename = string.Empty;
        public string KeyId { get; set; } = string.Empty;
        public RSAParameters Parameters { get; set; }

        public void Save()
            => File.WriteAllText(
                filename, 
                JsonConvert.SerializeObject(
                    this, 
                    new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() }));

        public static TemporaryRsaKey Get(string filename)
        {
            var result = JsonConvert.DeserializeObject<TemporaryRsaKey>(
                  File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),filename)),
                  new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() });
            result.filename = filename;
            return result;
        }
    }
}
