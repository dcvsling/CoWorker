
namespace CoWorker.Abstractions.TypeAccessor
{

    public interface IAccessor
    {
        object Get();
        void Set(object val);
    }
}