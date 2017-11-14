using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Linq;
using System.Reflection;
using System.Collections;
using IdentityServer4.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorker.Models.IdentityServer.ClientStores
{
    public class ClientConfigureOptions : IPostConfigureOptions<Client>
    {
        private readonly IConfiguration _config;

        public ClientConfigureOptions(IConfiguration config)
        {
            _config = config;
        }
        public void PostConfigure(string name, Client options)
        {
            var config = _config.GetSection($"identityserver:clients:{name}");
            if (!config.Exists()) return;
            config.Bind(options);
            options.ClientId = name;
            config.GetSection("clientsecrets")
                .Get<StringValues>()
                .Select(x => new Secret(x.Sha256()))
                .ToList()
                .ForEach(options.ClientSecrets.Add);
        }
    }


}
