using CoWorker.Primitives;

namespace CoWorker.Models.Abstractions.Stores
{
    public interface IDataProvider<T> : IName
    {
        T Value { get; }
    }
}
