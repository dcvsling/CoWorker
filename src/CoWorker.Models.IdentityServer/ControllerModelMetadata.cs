using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using IdentityServer4.EntityFramework.Entities;
using CoWorker.Models.Abstractions;
using System.Reflection;
using System.Linq;

namespace CoWorker.Models.IdentityServer
{

    public class ControllerModelMetadata : ApplicationPart, IApplicationPartTypeProvider
    {
        public override string Name => "GenericControllerModel";
        public IEnumerable<TypeInfo> Types => new Type[] { typeof(Client), typeof(ApiResource), typeof(IdentityResource),typeof(PersistedGrant) }.Select(x => x.GetTypeInfo());
    }
}
