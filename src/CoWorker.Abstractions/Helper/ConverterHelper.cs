using System.Threading.Tasks;
namespace System
{
    using System.Linq;

    public static class ConverterHelper
    {
        public static Lazy<T> ToLazy<T>(this Func<T> factory)
            => new Lazy<T>(factory);

        public static Lazy<T> ToLazy<T>(this Type type, params Func<object>[] argsGetter)
            where T : class
            => new Lazy<T>(
                () => Activator.CreateInstance(
                type, 
                argsGetter.Select(x => x()).ToArray()).As<T>());
        
        public static void IsType<TBase, TType>(
            this IOptional<TBase> obj, 
            Action<TType> truecase, 
            Action<TBase> falsecase)
            => (obj.Value is TType t 
                    ? Task.Run(() => truecase(t)) 
                    : Task.Run(() => falsecase(obj.Value)))
                .RunSynchronously();

        public static T As<T>(this object obj)
            => obj is T t ? t : default;
    }
}
