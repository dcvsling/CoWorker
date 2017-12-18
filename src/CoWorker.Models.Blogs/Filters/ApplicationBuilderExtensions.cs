using System;

namespace CoWorker.Models.Abstractions.Filters
{
    public static class ApplicationBuilderExtensions
    {
        public static T Next<T>(this T app,Action<T> next)
        {
            next(app);
            return app;
        }
    }
}
