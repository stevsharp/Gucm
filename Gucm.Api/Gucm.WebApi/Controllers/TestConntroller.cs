using System.Threading.Tasks;
using Common.Infrastructure.Bus;
using Common.Infrastructure.Notifications;
using Gucm.Api.Controllers;
using Gucm.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gucm.WebApi.Controllers
{
    public class TestConntroller : ApiControllerBase
    {

        public TestConntroller(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications) 
            : base(mediator, notifications) {}

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateGdprCommand request)
        {
            await _mediator.SendCommand(request);

            //return OkOrBadRequest(result, result.Model);

            return Response(request);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id , UpdateGdprCommand request)
        {
            if (id != request.Id)
                return BadRequest();

            await _mediator.SendCommand(request);

            return Response(request);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            //var result = await _mediator.Send(new DeleteGdprCommand() { Id = id } );

            //return NoContentOrBadRequest(result);

            return Ok();
        }
    }
}
