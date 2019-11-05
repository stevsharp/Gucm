
using Gucm.Domain.Models;
using MediatR;

namespace Gucm.Application.ViewModel
{
    public sealed class CreateGdprCommand : IRequest<BusinessResult<int>>
    {
        public int Id { get; set; }

        public string Gdpr { get; set; }
    }
}
