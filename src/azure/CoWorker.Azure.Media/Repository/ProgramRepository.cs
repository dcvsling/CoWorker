using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CoWorker.Azure.Media.Repository
{

    public class ProgramRepository : IRepository<IProgram>
    {
        private readonly ProgramBaseCollection _program;
        private readonly string _channelName;
        private readonly IRepository<IChannel> _channelRepo;
        private readonly IRepository<IAsset> _assetRepo;
        private readonly CloudMediaContext _context;

        public ProgramRepository(
            ProgramBaseCollection program,
            CloudMediaContext context,
            IRepository<IChannel> channelRepo,
            IRepository<IAsset> assetRepo)
        {
            _program = program;
            _channelRepo = channelRepo;
            _assetRepo = assetRepo;
            _context = context;
        }

        internal ProgramRepository(ProgramRepository repo, string channelName)
            : this(repo._program,repo._context,repo._channelRepo,repo._assetRepo)
        {
            _channelName = channelName;
        }
        private Task<IProgram> FindByName(string name)
            => Task.Run(() => _program.ToArray().FirstOrDefault(x => x.Name == $"{_channelName}-{name}"));

        async public Task<IProgram> CreateAsync(string name)
        {
            var programName = $"{_channelName}-{name}";
            var asset = await _assetRepo.CreateAsync(programName);
            var channel = await _channelRepo.FindAsync(_channelName);
            var program = await channel.Programs.CreateAsync(programName, TimeSpan.FromHours(8), asset.Id);
            var locator = _context.Locators.CreateLocator
            (
                LocatorType.OnDemandOrigin,
                asset,
                _context.AccessPolicies.Create
                (
                    "Live Stream Policy",
                    program.ArchiveWindowLength,
                    AccessPermissions.Read
                )
            );
            return program;
        }
        async public Task DeleteAsync(String name)
        {
            var program = await FindByName(name);
            if(ProgramState.Stopped != program.State) await program.StopAsync();
            await program.DeleteAsync();
        }
        public Task<IProgram> FindAsync(String name)
            => FindByName(name);
        public IEnumerable<IProgram> Query(Expression<Func<IProgram, bool>> predicate)
            => _program.Where(predicate);
        async public Task UpdateAsync(string name)
        {
            var Program = await FindByName(name);
            await (((ProgramState.Stopped | ProgramState.Stopping) & Program.State) == 0
                ? Program.StartAsync()
                : Program.StopAsync());
        }
    }
}
