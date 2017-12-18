using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using CoWorker.Primitives;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
namespace CoWorker.Models.Mvc
{

    public class MvcOptionsConfigureOptions : IConfigureOptions<MvcOptions>
    {
        private readonly IConfiguration _config;
        private readonly IEnumerable<IApplicationModelConvention> _conventions;

        public MvcOptionsConfigureOptions(
            IEnumerable<IApplicationModelConvention> conventions,
            IConfiguration config)
        {
            _config = config;
            _conventions = conventions;
        }

        public void Configure(MvcOptions options)
        {
            _conventions.Each(x => options.Conventions.Add(x));
            _config.GetSection(nameof(Mvc).ToLower()).Bind(options);
            options.AllowEmptyInputInBodyModelBinding = true;
            options.RequireHttpsPermanent = true;
            options.SslPort = 443;
        }
    }
}
