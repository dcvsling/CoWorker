namespace System
{
    using CoWorker.Primitives;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class Helper
    {
        public const string EmptyString = "";
        public static Action<T> Empty<T>() => x => { };
        public static Func<TResult> Default<TResult>(TResult result = default) => () => result;
        public static Func<T, TResult> Default<T, TResult>(TResult result = default) => x => Default(result)();
        public static void InitDefaultJsonSetting()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };
        }
    }
}
