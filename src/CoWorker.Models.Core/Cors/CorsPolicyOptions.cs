using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;
using CoWorker.Primitives;

namespace CoWorker.Models.Core.Cors
{

    public class CorsPolicyOptions
    {
        public string Name { get; set; }
        public bool Credentials { get; set; }
        public int MaxAge { get; set; }
        public List<string> ExposedHeaders { get; set; } = new List<string>();
        public List<string> Headers { get; set; } = new List<string>();
        public List<string> Methods { get; set; } = new List<string>();
        public List<string> Origins { get; set; } = new List<string>();
    }
}
