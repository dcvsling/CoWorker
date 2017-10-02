using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CoWorker.Azure.Media.Repository
{

    public class AssetRepository : IRepository<IAsset>
    {
        private readonly AssetBaseCollection _asset;
        private readonly CloudMediaContext _context;

        public AssetRepository(AssetBaseCollection asset, CloudMediaContext context)
        {
            _asset = asset;
            _context = context;
        }
        private Task<IAsset> FindByName(string name)
            => Task.Run(() => _asset.ToArray().FirstOrDefault(x => x.Name == name));
        async public Task<IAsset> CreateAsync(string name)
        {
            var asset = await _asset.CreateAsync(name, AssetCreationOptions.None, CancellationToken.None);
            IAssetDeliveryPolicy policy = await _context.AssetDeliveryPolicies.CreateAsync(
                    "Clear Policy",
                    AssetDeliveryPolicyType.NoDynamicEncryption,
                    AssetDeliveryProtocol.HLS | AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.Dash, 
                    null);

            asset.DeliveryPolicies.Add(policy);
            
            return asset;
        }
        public Task DeleteAsync(String name)
            => FindByName(name).Result.DeleteAsync();
        public Task<IAsset> FindAsync(String name) => FindByName(name);
        public IEnumerable<IAsset> Query(Expression<Func<IAsset, bool>> predicate)
            => _asset.Where(predicate);
        async public Task UpdateAsync(string name)
            => throw new NotImplementedException();
    }
}
