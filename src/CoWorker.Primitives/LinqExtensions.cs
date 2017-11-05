using System.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoWorker.Primitives
{
    public static class LinqExtensions
    {
        public static void Each<T>(this IEnumerable<T> seq,Action<T> action)
            => seq.ToList().ForEach(action);
    }

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
