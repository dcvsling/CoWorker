using System.Linq;
using System.Net.Http.Headers;
using System.Collections;
using System;
using System.Collections.Generic;

namespace CoWorker.Primitives
{
    public class Optional<T> : IEnumerable<T>,IEither<Optional<T>.None, Optional<T>.HasOne>
    {
        private IEnumerable<T> Content { get; }

        private Optional(IEnumerable<T> content)
        {
            this.Content = content;
        }

        public IEnumerator<T> GetEnumerator() => this.Content.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public IEither<TNewLeft, None> MapLeft<TNewLeft>(Func<HasOne, TNewLeft> mapping)
            => this

        public IEither<HasOne, TNewRight> MapRight<TNewRight>(Func<None, TNewRight> mapping)
        {
            throw new NotImplementedException();
        }

        public HasOne Reduce(Func<None, HasOne> mapping)
        {
            throw new NotImplementedException();
        }

        private class HasOne : IEither<T,HasOne>
        {

            internal HasOne(T value) {
                Value = value;
            }

            public T Value { get; }

            public IEither<TNewLeft, HasOne> MapLeft<TNewLeft>(Func<T, TNewLeft> mapping)
            {
                throw new NotImplementedException();
            }

            public IEither<None, TNewRight> MapRight<TNewRight>(Func<HasOne, TNewRight> mapping)
                => new  mapping(this);

            public T Reduce(Func<HasOne, T> mapping)
                => mapping(this);
        }

        private class None : IEither<None, T>
        {
            public IEnumerable<T> Value => Enumerable.Empty<T>();
        }
    }
}
