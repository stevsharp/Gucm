using Common.Infrastructure.Bus;
using Common.Infrastructure.Notifications;
using Gucm.Api.Models;
using Gucm.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Gucm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IMediatorHandler _mediator;

        protected readonly DomainNotificationHandler _notifications;

        protected ApiControllerBase(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;

            _notifications = (DomainNotificationHandler)notifications;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }

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
