using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Options;

namespace CoWorker.Models.IdentityServer.ClientStores
{
    public class ClientStore : IClientStore
    {
        private readonly IOptionsSnapshot<Client> _clients;

        public ClientStore(IOptionsSnapshot<Client> clients)
            =>_clients = clients;

        public Task<Client> FindClientByIdAsync(string clientId)
            => Task.FromResult(_clients.Get(clientId));
    }
}
