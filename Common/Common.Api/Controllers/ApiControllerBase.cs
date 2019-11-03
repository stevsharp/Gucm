using Gucm.Api.Models;
using Gucm.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Gucm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult OkOrBadRequest(BusinessResult businessResult)
        {
            if (!businessResult.IsSuccess())
                return BadRequestSerialized(businessResult);

            return Ok();
        }
        protected IActionResult OkOrBadRequest(BusinessResult businessResult, object model)
        {
            if (!businessResult.IsSuccess())
                return BadRequestSerialized(businessResult);

            return Ok(model);
        }

        protected IActionResult OkOrBadRequest<T>(BusinessResult<T> businessResult) where T : new()
        {
            if (!businessResult.IsSuccess())
                return BadRequestSerialized(businessResult);

            return Ok(businessResult.Model);
        }

        protected IActionResult NoContentOrBadRequest(BusinessResult businessResult)
        {
            if (!businessResult.IsSuccess())
                return BadRequestSerialized(businessResult);

            return NoContent();
        }

        private IActionResult BadRequestSerialized(BusinessResult businessResult) =>
                BadRequest(new ErrorResponse(businessResult.BrokenRules.Select(x => x.Rule)));

    }
}
