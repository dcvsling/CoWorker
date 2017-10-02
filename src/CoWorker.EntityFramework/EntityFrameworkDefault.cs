
using System;

namespace CoWorker.EntityFramework
{
    public static class EntityFrameworkDefault
    {
        public static string CreateDate = nameof(CreateDate);
        public static string ModifyDate = nameof(ModifyDate);
        public static string Creator = nameof(Creator);
        public static string Modifier = nameof(Modifier);
        public static string Id = nameof(Id);
        public static readonly DateTime MAX_DATE = new DateTime(2100, 12, 31);
        public static readonly DateTime MIN_DATE = new DateTime(1900, 1, 1);
        public static readonly DateTimeOffset MAX_DATEOFFSET = new DateTimeOffset(MAX_DATE);
        public static readonly DateTimeOffset MIN_DATEOFFSET = new DateTimeOffset(MIN_DATE);
    }
}
