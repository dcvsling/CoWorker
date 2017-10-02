using System.Net;
using Microsoft.Extensions.Options;
namespace CoWorker.Azure.Media.Repository
{
    using Microsoft.WindowsAzure.MediaServices.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ProgramRepository" />
    /// </summary>
    public class StreamingEndPointRepository : IRepository<IStreamingEndpoint>
    {
        /// <summary>
        /// Defines the _channelName
        /// </summary>
        private readonly string _channelName;
        
        private readonly StreamingEndpointBaseCollection _endpoint;
        private readonly IOptionsSnapshot<StreamingEndpointCreationOptions> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramRepository"/> class.
        /// </summary>
        /// <param name="program">The <see cref="ProgramBaseCollection"/></param>
        /// <param name="context">The <see cref="CloudMediaContext"/></param>
        /// <param name="channelRepo">The <see cref="IRepository{IChannel}"/></param>
        /// <param name="assetRepo">The <see cref="IRepository{IAsset}"/></param>
        public StreamingEndPointRepository(
            StreamingEndpointBaseCollection endpoint, 
            IOptionsSnapshot<StreamingEndpointCreationOptions> options)
        {
            _endpoint = endpoint;
            _options = options;
        }

        /// <summary>
        /// The FindAsync
        /// </summary>
        /// <param name="name">The <see cref="String"/></param>
        /// <returns>The <see cref="Task{IProgram}"/></returns>
        public Task<IStreamingEndpoint> FindAsync(String name)
            => FindByName(name);

        /// <summary>
        /// The Query
        /// </summary>
        /// <param name="predicate">The <see cref="Expression{Func{IProgram, bool}}"/></param>
        /// <returns>The <see cref="IEnumerable{IProgram}"/></returns>
        public IEnumerable<IStreamingEndpoint> Query(Expression<Func<IStreamingEndpoint, bool>> predicate)
            => _endpoint.Where(predicate);

        /// <summary>
        /// The CreateAsync
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{IProgram}"/></returns>
        public Task<IStreamingEndpoint> CreateAsync(string name)
            => _endpoint.CreateAsync(_options.Get(name));

        /// <summary>
        /// The DeleteAsync
        /// </summary>
        /// <param name="name">The <see cref="String"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task DeleteAsync(String name)
        {
            var endpoint = await FindByName(name);
            if (0 == ((StreamingEndpointState.Stopped | StreamingEndpointState.Stopping) & endpoint.State))
                await endpoint.StopAsync();
            await endpoint.DeleteAsync();
        }

        /// <summary>
        /// The UpdateAsync
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdateAsync(string name)
        {
            var endpoint = await FindByName(name);
            var _ = endpoint.ScaleAsync(endpoint.ScaleUnits.Value + 1);
        }

        /// <summary>
        /// The FindByName
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{IProgram}"/></returns>
        private Task<IStreamingEndpoint> FindByName(string name)
            => Task.Run(() => _endpoint.ToArray().FirstOrDefault(x => x.Name == name));
    }
}