using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoWorker.Abstractions.Values
{
    
    public struct Condition<T>
    {
        private Func<T, bool> _condition;
        public Condition(Func<T, bool> condition)
        {
            _condition = condition ?? (t => false);
        }

        public Func<T, bool> Invoke
            => _condition;

        public static Condition<T> operator &(Condition<T> left, Condition<T> right)
            => new Condition<T>(t => left.Invoke(t) && right.Invoke(t));
        public static Condition<T> operator |(Condition<T> left, Condition<T> right)
            => new Condition<T>(t => left.Invoke(t) || right.Invoke(t));
    }

}
