using System.Threading.Tasks;
using Gucm.Api.Controllers;
using Gucm.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gucm.WebApi.Controllers
{
    public class TestConntroller : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public TestConntroller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateGdprCommand request)
        {
            var result =  await _mediator.Send(request);

            return OkOrBadRequest(result, result.Model);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id , UpdateGdprCommand request)
        {
            if (id != request.Id)
                return BadRequest();

            var result = await _mediator.Send(request);

            return NoContentOrBadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteGdprCommand() { Id = id } );

            return NoContentOrBadRequest(result);
        }
    }
}
