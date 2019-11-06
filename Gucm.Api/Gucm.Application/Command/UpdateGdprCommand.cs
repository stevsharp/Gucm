
using Gucm.Domain.Models;
using MediatR;

namespace Gucm.Application.ViewModel
{

    public sealed class UpdateGdprCommand : IRequest<BusinessResult<bool>>
    {
        public int Id { get; set; }

        public string Gdpr { get; set; }
    }
}
