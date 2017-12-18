namespace System
{
    using Newtonsoft.Json;

    public static class Helper
    {
        public const string EmptyString = "";
        public static Action<T> Empty<T>() => x => { };
        public static Action Empty(Action action = default) => action ?? (() => { });
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
