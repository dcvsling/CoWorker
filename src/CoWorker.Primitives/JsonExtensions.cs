using System.Linq;
using Newtonsoft.Json;

namespace CoWorker.Primitives
{

    public static class JsonExtensions
    {
        public static JsonSerializerSettings Initialize(this JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            settings.TypeNameHandling = TypeNameHandling.None;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Converters = settings.Converters.Distinct().ToList();
            return settings;
        }
    }
}
