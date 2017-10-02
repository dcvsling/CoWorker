
namespace CoWorker.Abstractions.TypeAccessor
{

    public class Accessor : IAccessor
    {
        private readonly object _obj;
        private readonly IPropertyAccessor _accessor;

        internal Accessor(IPropertyAccessor accessor,object obj)
        {
            _obj = obj;
            _accessor = accessor;
        }
        public System.Object Get()
            => _accessor.Get(_obj);
        public void Set(System.Object val)
            => _accessor.Set(_obj, val);
    }
}