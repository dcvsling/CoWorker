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
    public class GenericControllerModelConvention : IApplicationModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType) return;

            controller.ControllerName = controller.ControllerType.Name.ToLower().Split(new string[] { nameof(controller) },StringSplitOptions.RemoveEmptyEntries).First();

            controller.RouteValues.Add(nameof(controller), controller.ControllerName);

            controller.ControllerType
                .GenericTypeArguments
                .Aggregate(controller.ControllerType.GetGenericTypeDefinition().GetTypeInfo().GenericTypeParameters.AsEnumerable(),
                (seed, next) =>
                {
                    controller.RouteValues.Add(seed.First().Name.Substring(1).ToLower(), next.Name.ToLower());
                    return seed.Skip(1);
                });

            controller.ControllerName = controller.ApiExplorer.GroupName = string.Join("_",controller.RouteValues.Values);

            controller.Selectors.Each(x => x.AttributeRouteModel.Template
                = controller.Attributes
                    .Select(y => y is RouteAttribute route ? route.Template : string.Empty)
                    .First(y => !string.IsNullOrEmpty(y)));

            controller.Actions.Each(Apply);
        }

        public void Apply(ApplicationModel application)
            => application.Controllers.Each(x => Apply(x));

        public void Apply(ActionModel action)
        {
            action.ActionName = $"{action.Controller.ControllerName}_{action.ActionName}{string.Join("_", action.Parameters.Select(x => x.ParameterName))}";
            action.ApiExplorer.GroupName = action.ActionName;
            action.Selectors.Each(x => x.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(action.Controller.Selectors.FirstOrDefault()?.AttributeRouteModel, x.AttributeRouteModel));
            action.Selectors.Each(x => x.ActionConstraints.Add(new RESTActionConstraint(action.ActionMethod.Name)));
        }
    }
}
