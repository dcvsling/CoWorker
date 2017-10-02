
namespace CoWorker.Abstractions.Values
{
    using System;
    public struct DateTimeRange : IEquatable<DateTimeRange>
    {
        public static DateTimeRange Create(DateTime start, DateTime end)
            => new DateTimeRange(start, end);
        public static DateTimeRange Create(DateTime start, TimeSpan range)
            => new DateTimeRange(start, start.Add(range));

        private DateTimeRange(DateTime start,DateTime end)
        {
            this.Start = start;
            this.End = end;
        }
        public DateTime Start { get; }
        public DateTime End { get; }

        public Boolean Equals(DateTimeRange other) => Start == other.Start && End == other.End;
    }
}
