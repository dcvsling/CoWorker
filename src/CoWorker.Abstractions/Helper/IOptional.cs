using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System
{
    public interface IOptional<T> : IEnumerable<T>, IEquatable<T>
    {
		T Value { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        [EditorBrowsable(EditorBrowsableState.Never)]
        String ToString();

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}