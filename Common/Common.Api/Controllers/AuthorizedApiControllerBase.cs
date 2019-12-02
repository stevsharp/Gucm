using Common.Infrastructure.Bus;
using Common.Infrastructure.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Gucm.Api.Controllers
{
    [Authorize]
    public class AuthorizedApiControllerBase : ApiControllerBase
    {
        public AuthorizedApiControllerBase(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications) : base(mediator, notifications)
        {
        }
    }
}
