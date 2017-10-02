using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorker.Abstractions.Cache
{
    public interface IOptionsCache<TOptions> : IOptionsMonitorCache<TOptions> where TOptions : class
    {
    }

    public class ObjectCache<TOptions> : OptionsCache<TOptions>,IOptionsCache<TOptions> where TOptions : class
    {   
    }
}
