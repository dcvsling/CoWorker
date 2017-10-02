using System.Collections.Generic;
using System.Linq;

namespace System.Threading.Tasks
{
    public static class AsyncHelper
    {
        public static Task RunAsync(this Action action)
            => Task.Run(action);

        public static Task RunAsync<T>(this Action<T> action,Func<T> t)
            => Task.Run(() => action(t()));

        public static Task<T> RunAsync<T>(this Func<T> func)
            => Task.Run(func);

        public static Func<Task> ToAsync(this Action action)
            => () => action.RunAsync();

        public static Func<T,Task> ToAsync<T>(this Action<T> action)
            => x => action.RunAsync(() => x);

        public static Func<T, Task> Empty<T>() => x => Task.CompletedTask;

        public static Task<TResult> UsingAsync<TDisposable, TResult>(
            this TDisposable disposer,
            Func<TDisposable, TResult> func,
            Action<Exception> error = null,
            Action final = null)
            where TDisposable : IDisposable
            => Task.Run(() => disposer.Using(func,error,final));

        public static Task UsingAsync<TDisposable>(
            this TDisposable disposer,
            Action<TDisposable> action,
            Action<Exception> error = null,
            Action final = null)
            where TDisposable : IDisposable
                => Task.Run(() => disposer.Using(action,error,final));

        async public static Task<T> DoAsync<T>(this IOptional<T> t, Action<T> action)
        {
            await Task.Run(() => action(t.Value));
            return t.Value;
        }

        public static Task CallbackAsync(this Action callback, Action action)
            => Task.Run(() => {
                action();
                callback?.Invoke();
            });

        public static Task WaitAll(this IEnumerable<Task> tasks)
        {
            Task.WaitAll(tasks.ToArray());
            return Task.CompletedTask;
        }

        public static Task<int> WaitAny(this IEnumerable<Task> tasks)
            => Task.FromResult(Task.WaitAny(tasks.ToArray()));
    }
}
