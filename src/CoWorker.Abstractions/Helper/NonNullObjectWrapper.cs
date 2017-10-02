
namespace CoWorker.Abstractions.Helper
{
    using System.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
	internal class NonNullObjectWrapper<T> : IOptional<T>
	{
        private IEnumerable<T> _enumerable;
		public NonNullObjectWrapper(T t)
            => this._enumerable = Helper.Group(t);
        public T Value => _enumerable.Any() ? _enumerable.First() : default;

        public Boolean Equals(T other) => Value.Equals(other);
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => _enumerable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => (this as IEnumerable<T>).GetEnumerator();
    }
}
