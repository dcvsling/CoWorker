using System.Runtime.Loader;
using CoWorker.Primitives;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CoWorker.Models.Abstractions.ApplicationParts
{
    public class ApplicationPart : IName
    {
        private readonly AssemblyName _name;
        private readonly Task<Assembly> _assembly;
        public ApplicationPart(AssemblyName name)
        {
            _name = name;
            _assembly = Task.Run(() => AssemblyLoadContext.Default.LoadFromAssemblyName(name));
        }
        public string Name => _name.Name;
        public IEnumerable<Type> Types => _assembly.Result.ExportedTypes;
    }
}
