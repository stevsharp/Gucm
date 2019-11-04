using Gucm.Api.Controllers;
using Gucm.Data;
using Gucm.Data.Context;
using Microsoft.AspNet.OData.Routing;

namespace Gucm.WebApi.Controllers.OData
{
    /// <summary>
    /// http://localhost:13231/odata/$metadata
    /// http://localhost:13231/odata/odata/GdprTable(1)
    /// http://localhost:13231/odata/odata/GdprTable
    /// </summary>
    [ODataRoutePrefix("GdprTable")]
    public class GdprTableController : BaseODataController<GdprTable>
    {
        public GdprTableController(ODataDbContext dbContext) : base(dbContext) {}
    }
}