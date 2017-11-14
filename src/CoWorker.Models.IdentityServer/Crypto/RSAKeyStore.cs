using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CoWorker.Models.IdentityServer.Crypto
{
    public class RSAKeyStore
    {
        private readonly IConfiguration _config;

        public RSAKeyStore(IConfiguration config)
        {
            _config = config;
        }

        public RsaSecurityKey Create()
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(Convert.FromBase64String(_config["SigningCredential"]));
            return new RsaSecurityKey(rsa);
        }
    }
}
