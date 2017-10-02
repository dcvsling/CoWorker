using Microsoft.Extensions.Internal;
using System;
namespace CoWorker.EntityFramework.ValueGenerators
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    public class DatetimeValueGenerator : ValueGenerator<DateTime>
    {
        private readonly ISystemClock _clock;

        public DatetimeValueGenerator(ISystemClock clock)
        {
            this._clock = clock;
        }
        public override Boolean GeneratesTemporaryValues => false;

        public override DateTime Next(EntityEntry entry)
            => _clock.UtcNow.DateTime;
    }


}
