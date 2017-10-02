using System.Reflection;

namespace CoWorker.Abstractions.TypeStore
{

    public static class TypeStoreExtensions
    {
        public static Assembly Load(this AssemblyName name) => Assembly.Load(name);
    }
}
