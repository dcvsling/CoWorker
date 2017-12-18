using System.Net;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Models.Abstractions.Stores
{
    public static partial class RSAExtensions
    {
        public static IServiceCollection AddDefaultRsaKey(this IServiceCollection services)
            => services.AddSingleton<IDataProvider<RsaSecurityKey>>(p => new DefaultDataProvider<RsaSecurityKey>(string.Empty,() => CreateRsaKey()));
        internal static RsaSecurityKey CreateRsaKey(string filename = default, bool persistKey = true)
        {
            filename = filename ?? Path.Combine(Directory.GetCurrentDirectory(), "tempkey.rsa");
            return File.Exists(filename) ? CreateByExistFile(filename) : CreateByEmpty().SaveRsaKey(persistKey ? filename : string.Empty);
        }
        
        internal static RsaSecurityKey SaveRsaKey(this RsaSecurityKey key,string filename)
        {
            if (string.IsNullOrEmpty(filename)) return key;
            File.WriteAllText(
                filename, 
                JsonConvert.SerializeObject(new TemporaryRsaKey
                {
                    Parameters = key.Rsa?.ExportParameters(includePrivateParameters: true) ?? key.Parameters,
                    KeyId = key.KeyId
                }, 
                new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() }));
            return key;
        }

        internal static RsaSecurityKey CreateByEmpty()
        {
            var rsa = RSA.Create();
            RsaSecurityKey key = rsa is RSACryptoServiceProvider provider
                ? CreateByRsaCng(provider)
                : CreateByRsa(rsa);
            key.KeyId = CryptoRandom.CreateUniqueId(16);
            return key;
        }

        private static RsaSecurityKey CreateByRsa(RSA rsa)
        {
            rsa.KeySize = 2048;
            return new RsaSecurityKey(rsa);
        }

        private static RsaSecurityKey CreateByRsaCng(RSACryptoServiceProvider provider)
        {
            provider.Dispose();
            return new RsaSecurityKey(new RSACng(2048).ExportParameters(includePrivateParameters: true));
        }

        internal static RsaSecurityKey CreateByExistFile(string filename)
        {
            var keyFile = File.ReadAllText(filename);
            var tempKey = JsonConvert.DeserializeObject<TemporaryRsaKey>(keyFile, new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() });
            return CreateKey(tempKey);
        }

        /// <summary>
        /// Creates an RSA security key.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal static RsaSecurityKey CreateKey(this TemporaryRsaKey key)
            => new RsaSecurityKey(key.Parameters)
            {
                KeyId = key.KeyId
            };
    }
}
