using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Composition.Convention;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Runtime.Loader;
using System.Composition.Hosting;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.FileSystemGlobbing;

namespace CoWorker.Models.HostingStartupBase
{

    public class HostingStartupProvider
    {
        private Task<IEnumerable<Assembly>> _assemblies;
        private ContainerConfiguration _config;
        private IEnumerable<IHostingStartup> startups;
        public HostingStartupProvider()
        {
            _config = new ContainerConfiguration();
            _assemblies = Task.Run(ImportAssembly);
        }

        public IEnumerable<IHostingStartup> HostingStartups
            => startups ?? GetHostingStartups();

        private IEnumerable<IHostingStartup> GetHostingStartups()
        {
            startups = CreateHost().GetExports<IHostingStartup>();
            return startups;
        }


        private CompositionHost CreateHost()
        {
            _config = new ContainerConfiguration();
            var convention = new ConventionBuilder();
            convention.ForTypesMatching<IHostingStartup>(
                t => typeof(IHostingStartup).IsAssignableFrom(t) && t != typeof(BootstrappingHostingStartup))
                .Export<IHostingStartup>()
                .NotifyImportsSatisfied(t => true);
            _config.WithAssemblies(_assemblies.Result, convention);
            return _config.CreateContainer();
        }

        async private Task<IEnumerable<Assembly>> ImportAssembly()
            => await DependencyContext.Default
                .GetDefaultAssemblyNames()
                .Where(x => x.Name.StartsWith(nameof(CoWorker), StringComparison.OrdinalIgnoreCase)
                    && x != this.GetType().Assembly.GetName())
                .ToAsyncEnumerable()
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyName)
                .ToList();

        //private void ImportAssembly(string path)
        //{
        //    if (!Directory.Exists(path)) return;
        //    var matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
        //    matcher.AddIncludePatterns(new string[] { "**/CoWorker**.dll" });
        //    matcher.AddExcludePatterns(_assemblies.Select(x => $"**/{x.CodeBase.Replace("\\","/").Split('/').Last()}"));
        //    var files = matcher.GetResultsInFullPath(path);
        //    GetNewest(files)
        //        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
        //        .Each(_assemblies.Add);
        //}

        //private IEnumerable<string> GetNewest(IEnumerable<string> assembyFiles)
        //    => assembyFiles.Select(x => new FileInfo(x))
        //        .GroupBy(x => x.Name)
        //        .Select(x => x.FirstOrDefault(z => z.LastWriteTime == x.Max(y => y.LastWriteTime)))
        //        .Select(x => x?.FullName);
    }
}
