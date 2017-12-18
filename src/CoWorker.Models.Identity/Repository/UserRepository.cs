
using CoWorker.Models.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoWorker.Models.Identity.Repository
{
    public class UserRepository<TModel> : DatabaseRepository<UserDbContext,TModel> where TModel: class,new()
    {
        public UserRepository(Microsoft.EntityFrameworkCore.Internal.DbContextPool<UserDbContext> pool) : base(pool)
        {
        }
    }

    public class ControllerModelMetadata : ApplicationPart, IApplicationPartTypeProvider
    {
        public override string Name => "GenericControllerModel";
        public IEnumerable<TypeInfo> Types => new Type[] {
            typeof(User),
            typeof(UserClaim),
            typeof(UserToken),
            typeof(UserLogin)
        }.Select(x => x.GetTypeInfo());
    }
}
