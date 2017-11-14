using System;
using System.Collections.Generic;

namespace CoWorker.Models.Abstractions.ApplicationParts
{

    public class Feature
    {
        public List<Type> Types { get; } = new List<Type>();
    }
}
