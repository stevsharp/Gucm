using Microsoft.AspNetCore.Authorization;

namespace Gucm.Api.Controllers
{
    [Authorize]
    public class AuthorizedApiControllerBase : ApiControllerBase
    {

    }
}
