using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData.Routing;
using System;
using System.Linq.Expressions;
using System.Linq;
using Gucm.Data.EntityFramework.BaseDbContext;

namespace Gucm.Api.Controllers
{
    //[Authorize]
    public abstract class BaseODataController : ODataController
    {
        protected readonly BaseDbContext dbContext;

        protected BaseODataController(BaseDbContext dbContext) => this.dbContext = dbContext;
    }

    //[Authorize]
    public abstract class BaseODataController<T> : ODataController where T : class
    {
        protected readonly BaseDbContext dbContext;

        protected BaseODataController(BaseDbContext dbContext) => this.dbContext = dbContext;

        [ODataRoute]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public virtual IActionResult Get() => Ok(dbContext.Set<T>());

        [ODataRoute("({key})")]
        [EnableQuery]
        public virtual SingleResult<T> Get([FromODataUri] int key)
        {
            var keyName = this.GetKey(typeof(T).FullName);
            var propertyType = this.GetKeyPropertyType(keyName);

            object objectKey = propertyType == typeof(Int64) ? (Int64)key : (object)key;

            var param = Expression.Parameter(typeof(T));
            var condition =
                Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(
                        Expression.Property(param, keyName),
                        Expression.Constant(objectKey)
                    ),
                    param
                );

            return SingleResult.Create(dbContext.Set<T>().Where(condition).AsQueryable());
        }

        protected virtual Type GetKeyPropertyType(string prop)
        {
            var propertyType = typeof(T).GetProperty(prop).PropertyType;

            return propertyType;
        }

        protected virtual String GetKey(string entry)
        {
            var keyName = dbContext.Model.FindEntityType(entry).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            return keyName;
        }

    }
}
