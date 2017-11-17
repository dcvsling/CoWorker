using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using System.Reflection;
using System.Dynamic;
using System.Linq.Expressions;

namespace CoWorker.Dashboard
{
    public class DashboardHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(ConfigureServices);
        }

        public void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            services.AddMvc().AddViewOptions(o => { });
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentAttribute : Attribute, ITagHelper
    {
        public ComponentAttribute(Component component)
        {

        }

        public int Order { get; }

        public void Init(TagHelperContext context)
        {

        }

        public Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            return Task.CompletedTask;
        }
    }

    public interface ITagServiceCollection : IServiceCollection { }
    public interface ITagProvider : IServiceProvider { }


    public struct Component
    {
        public string selectorId;
        public string template;
        public string view;
    }
    [Component("","","")]
    public class TagHelperDiscovery
    {

    }

    public class TagHelperFeatureProvider : IApplicationFeatureProvider<ComponentAttribute>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ComponentAttribute feature)
        {


            parts.SelectMany(x => x is AssemblyPart assembly ? assembly.Types : Type.EmptyTypes.Select(y => y.GetTypeInfo()));
        }
    }

    [StructLayout(LayoutKind.Auto)]
    public struct ValueObject : ITuple, IDynamicMetaObjectProvider
    {

        public object this[int index] => throw new NotImplementedException();

        public int Length => throw new NotImplementedException();

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            throw new NotImplementedException();

        }


    }
}
