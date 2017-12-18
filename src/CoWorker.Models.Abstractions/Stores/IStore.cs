namespace CoWorker.Models.Abstractions.Stores
{
    public interface IStore<T> where T : class
    {
        T Get(string name);
    }
}