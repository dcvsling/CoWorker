using System.Linq;
namespace Newtonsoft.Json
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static partial class JsonHelper
    {
        public static string ToJson(this object obj)
            => JsonConvert.SerializeObject(obj);

        public static T ToObject<T>(this string json)
            => JsonConvert.DeserializeObject<T>(json);

        public static T Bind<T>(this string json, T t)
        {
            JsonConvert.PopulateObject(json, t);
            return t;
        }

        public static JsonSerializerSettings Initialize(this JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            settings.TypeNameHandling = TypeNameHandling.None;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Converters.Add(GuidConverter.Default);
            settings.Converters = settings.Converters.Distinct().ToList();
            return settings;
        }
    }
}
