
using Gucm.Domain.Models;
using MediatR;

namespace Gucm.Application.ViewModel
{

    public sealed class DeleteGdprCommand : IRequest<BusinessResult<bool>>
    {
        public int Id { get; set; }
    }
}
