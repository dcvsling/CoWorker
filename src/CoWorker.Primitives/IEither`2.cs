using System.Net.Http.Headers;
using System.Collections;
using System;
using System.Collections.Generic;

namespace CoWorker.Primitives
{

    public interface IEither<TLeft,out TRight>
    {
        IEither<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping);
        IEither<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping);
        TLeft Reduce(Func<TRight, TLeft> mapping);
    }
    

    class Left<TLeft, TRight> : IEither<TLeft, TRight>
    {
        TLeft Value { get; }

        public Left(TLeft value)
        {
            this.Value = value;
        }

        public IEither<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping)
            => new Left<TNewLeft, TRight>(mapping(this.Value));

        public IEither<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping)
            => new Left<TLeft, TNewRight>(this.Value);

        public TLeft Reduce(Func<TRight, TLeft> mapping) =>
            this.Value;
    }

    class Right<TLeft, TRight> : IEither<TLeft, TRight>
    {
        TRight Value { get; }

        public Right(TRight value)
        {
            this.Value = value;
        }

        public IEither<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping)
            => new Right<TNewLeft, TRight>(this.Value);

        public IEither<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping)
            => new Right<TLeft, TNewRight>(mapping(this.Value));

        public TLeft Reduce(Func<TRight, TLeft> mapping)
            => mapping(this.Value);
    }

    public interface ISpec<out T>
    {
        T Build();
    }
    public interface IValidator<T,out TMeta> : ISpec<TMeta>
    {
    }

    //public class ValidatorBuilder<T>
    //{
    //    private IDictionary<>
    //    public ValidatorBuilder<T> AddValid<TMember>(Func<TMember,bool> valid)
    //    {

    //    }
    //    static Func<T, Action> NotNull<T>() => t => ReferenceEquals(t, null) ? () => throw new NullReferenceException() : Helper.Empty();
    //}
}
