using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Reflection;
using System.Linq;
using CoWorker.Primitives;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;

namespace CoWorker.Models.Mvc
{

    public class RESTActionConstraint : IActionConstraint
    {
        private readonly string[] _methods;

        public RESTActionConstraint(params string[] methods)
        {
            _methods = methods;
        }

        public int Order => 100;

        public bool Accept(ActionConstraintContext context)
            => _methods.Contains(
                context.RouteContext.HttpContext.Request.Method,
                EqualityComparer<string>.Create(
                    x => x.ToLower().GetHashCode(),
                    (a, b) => a.Equals(b, StringComparison.OrdinalIgnoreCase)));
    }
}
