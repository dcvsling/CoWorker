
namespace CoWorker.Abstractions.Helper
{
    using System.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
	internal class Optional<T> : IOptional<T>
	{
        private IEnumerable<T> _enumerable;
		public Optional(T t)
            => this._enumerable = Helper.Group(t);
        public T Value => _enumerable.Any() ? _enumerable.First() : default;
        public IEnumerator<T> GetEnumerator()
            => _enumerable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => this.AsEnumerable().GetEnumerator();

        public static implicit operator T(Optional<T> optionsal) => optionsal.Value;

        public static implicit operator Optional<T>(T t) => new Optional<T>(t);
        public static bool operator ==(Optional<T> left, Optional<T> right)
            => left.Value.Equals(right.Value);
        public static bool operator !=(Optional<T> left, Optional<T> right)
            => !left.Value.Equals(right.Value);
        public Boolean Equals(Optional<T> other) => other.Value.Equals(this.Value);
        public override Boolean Equals(Object obj) => (obj is Optional<T> optional
                ? optional.Value
                : obj)
            .Equals(this.Value);
        public override Int32 GetHashCode() => Value?.GetHashCode() ?? this.GetHashCode();
        public Boolean Equals(T other)
            => this.Value.Equals(other);
    }
}
