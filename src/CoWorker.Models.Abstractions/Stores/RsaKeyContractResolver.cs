using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace CoWorker.Models.Abstractions.Stores
{
    internal class RsaKeyContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.Ignored = false;

            return property;
        }
    }
}
