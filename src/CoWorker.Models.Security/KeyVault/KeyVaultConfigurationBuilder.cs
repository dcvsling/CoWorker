using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace CoWorker.Models.Security.KeyVault
{
    using CoWorker.Primitives;

    public class KeyVaultConfigurationBuilder
    {
        private const string AZURE_KEYVAULT_URL = "https://{0}.vault.azure.net/";

        public IConfigurationBuilder Build(IConfigurationBuilder options)
            => options.Build().GetSection("keyvault").GetChildren()
                .Where(x => x.Exists())
                .Aggregate(options,SetKeyVaultSource);

        private void SetKeyVaultSource(IConfigurationBuilder builder, IConfigurationSection config)
            => builder.AddAzureKeyVault(
                String.Format(AZURE_KEYVAULT_URL, config.GetValue<string>("name")),
                config.GetValue<string>("clientid"),
                config.GetValue<string>("clientsecret"));
    }


}
