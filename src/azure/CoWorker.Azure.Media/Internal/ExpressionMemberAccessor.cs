using CoWorker.Abstractions.Cache;
using System;

namespace CoWorker.Azure.Media.Internal
{
    public class MemberAccessor<TBase>
    {
        private IOptionsCache<IPropertyAccessor> _cache;
        public MemberAccessor()
        {
            _cache = new ObjectCache<IPropertyAccessor>();
        }

        public IPropertyAccessor Create(TBase parent)
            => new PropertyAccessor(parent);
    }

    public interface IPropertyAccessor
    {
        object Get(object parent);
        void Set(object parent, object val);
    }

    public class PropertyAccessor : IPropertyAccessor
    {
        private readonly object _parent;
        public PropertyAccessor(object parent)
        {
            _parent = parent;
        }

        public Object Get(Object parent) => throw new NotImplementedException();
        public void Set(Object parent, Object val) => throw new NotImplementedException();
    }
}
