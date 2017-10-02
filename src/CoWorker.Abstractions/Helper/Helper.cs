namespace System
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class Helper
    {
        public const string EmptyString = "";
        public static T[] EmptyArray<T>() => new T[] { };
        public static IEnumerable<T> Group<T>(params T[] ts) => ts;

        public static Func<object> CreateFactory(Type type)
            => () => Activator.CreateInstance(type);
        public static Func<T> CreateFactory<T>()
            => () => CreateFactory(typeof(T)).As<T>();
        public static Func<object> CreateFactory(Type type,params object[] args)
            => () => Activator.CreateInstance(type,args);
        public static Func<T> CreateFactory<T>(params object[] args)
            => () => CreateFactory(typeof(T), args).As<T>();

        public static Action<T> If<T>(
          Func<T, bool> predicate,
          Action<T> truecase,
          Action<T> falsecase = null)
          => t => (predicate?.Invoke(t) ?? false ? truecase : falsecase)?.Invoke(t);

        public static Func<T, TResult> If<T, TResult>(
            Func<T, bool> predicate,
            Func<T, TResult> truecase,
            Func<T, TResult> falsecase = null)
            => t => ((predicate?.Invoke(t) ?? false ? truecase : falsecase) ?? (x => default))(t);

        public static TDelegate Create<TDelegate>(TDelegate invoker) => invoker;

        private readonly static Action EmptyAction = () => { };
        public static Action Empty() => EmptyAction;
        public static Action<T> Empty<T>() => x => EmptyAction();
        public static Action<T, T2> Empty<T, T2>() => (x, y) => EmptyAction();
        private readonly static Func<object> DefaultMethod = () => default;
        public static Func<object> Default() => DefaultMethod;
        public static Func<TResult> Default<TResult>(TResult result = default) => () => result;
        public static Func<T, TResult> Default<T, TResult>(TResult result = default) => x => Default(result)();
        public static Func<T, T2, TResult> Default<T, T2, TResult>(TResult result = default) => (x, y) => Default(result)();
        public static void InitDefaultJsonSetting()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Converters = { GuidConverter.Default }
            };
        }
    }
}
