namespace CoWorker.Models.Abstractions.ApplicationParts
{
    public interface IApplicationFeatureFactory
    {
        Feature Create(string name);
    }
}