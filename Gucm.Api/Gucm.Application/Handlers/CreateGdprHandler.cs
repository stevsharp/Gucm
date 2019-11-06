using Common.Infrastructure.UnitOfWork;
using Gucm.Application.ViewModel;
using Gucm.Domain.Gdpr;
using Gucm.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gucm.Application.Handlers
{
    public class CreateGdprHandler : IRequestHandler<CreateGdprCommand, BusinessResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IGdprDomainRepository _gdprDomainRepository;

        public CreateGdprHandler(IUnitOfWork unitOfWork, IGdprDomainRepository gdprDomainRepository)
        {
            _unitOfWork = unitOfWork;

            _gdprDomainRepository = gdprDomainRepository;
        }

        public async Task<BusinessResult<int>> Handle(CreateGdprCommand request, CancellationToken cancellationToken)
        {
            var result = new BusinessResult<int>();

            var domain = new GdprDomain(request.Gdpr);

            var bcErrors = domain.GetBrokenRules();
            if (bcErrors.Count > 0)
            {
                result.AddBrokenRule(bcErrors);
                return result;
            }
            
            _gdprDomainRepository.Add(domain);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            result.Model = domain.Id;
            
            return result;
        }
    }
}
