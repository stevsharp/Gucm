using Gucm.Application.ViewModel;
using Gucm.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application.Handlers
{
    public class CreateGdprHandler : IRequestHandler<CreateGdprCommand, BusinessResult<bool>>
    {
        public async Task<BusinessResult<bool>> Handle(CreateGdprCommand request, CancellationToken cancellationToken)
        {
            return new BusinessResult<bool>();
        }
    }
}
