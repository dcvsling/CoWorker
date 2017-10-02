using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using System;

namespace CoWorker.Azure.Media.Internal
{

    public class AzureResourceControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType) return;
            controller.ControllerName = controller.ControllerType.Name
                .Split(new string[] { "Controller" }, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault();
            controller.RouteValues.Add(
                "resource",
                controller.ControllerType.GenericTypeParameters.FirstOrDefault().Name);
        }
    }
}
