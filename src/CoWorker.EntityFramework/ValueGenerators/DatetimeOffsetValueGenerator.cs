using Microsoft.Extensions.Internal;
using System;
namespace CoWorker.EntityFramework.ValueGenerators
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    public class DatetimeOffsetValueGenerator : ValueGenerator<DateTimeOffset>
    {
        private readonly ISystemClock _clock;

        public DatetimeOffsetValueGenerator(ISystemClock clock)
        {
            this._clock = clock;
        }
        public override Boolean GeneratesTemporaryValues => false;

        public override DateTimeOffset Next(EntityEntry entry)
            => _clock.UtcNow;
    }


}
