using Common.Infrastructure.UnitOfWork;
using Gucm.Application.ViewModel;
using Gucm.Domain.Gdpr;
using Gucm.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Gucm.Application;
using Common.Infrastructure.Bus;
using Common.Infrastructure.Notifications;
using Gucm.Application.Events;

namespace Gucm.Application.Handlers
{
    public class CreateGdprHandler : CommandHandler,  IRequestHandler<CreateGdprCommand, bool>
    {
        private readonly IGdprDomainRepository _gdprDomainRepository;

        public CreateGdprHandler(IUnitOfWork uow, IGdprDomainRepository gdprDomainRepository , 
            IMediatorHandler bus, INotificationHandler<DomainNotification> notifications) 
            : base(uow, bus, notifications)
        {
            _gdprDomainRepository = gdprDomainRepository;
        }

        public async Task<bool> Handle(CreateGdprCommand request, CancellationToken cancellationToken)
        {
            var result = new BusinessResult<bool>();

            var commandIsValid = request.IsValid();

            if (!commandIsValid)
            {
                NotifyValidationErrors(request);
                return false;
            }

            var domain = new GdprDomain(request.Gdpr);

            var bcErrors = domain.GetBrokenRules();
            if (bcErrors.Count > 0)
            {
                result.AddBrokenRule(bcErrors);

                foreach (var errBc in bcErrors)
                    request.ValidationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(errBc.Rule, errBc.Property));
                
                return false;
            }

            _gdprDomainRepository.Add(domain);

            if (await Commit(cancellationToken))
                _bus.RaiseEvent(new GdprCreated(domain.Id));

            
            result.Model = true;

            return true;
        }

        //public async Task<bool> Handle(CreateGdprCommand request, CancellationToken cancellationToken)
        //{
        //    var result = new BusinessResult<bool>();

        //    var commandIsValid = request.IsValid();

        //    if (!commandIsValid)
        //    {
        //        NotifyValidationErrors(request);
        //        return false;
        //    }

        //    var domain = new GdprDomain(request.Gdpr);

        //    var bcErrors = domain.GetBrokenRules();
        //    if (bcErrors.Count > 0)
        //    {
        //        result.AddBrokenRule(bcErrors);
        //        return false;
        //    }

        //    _gdprDomainRepository.Add(domain);

        //    if (Commit())
        //        _bus.RaiseEvent(new GdprCreated(domain.Id));


        //    result.Model = true;

        //    return true;
        //}




        //public CreateGdprHandler(IUnitOfWork unitOfWork, IGdprDomainRepository gdprDomainRepository)
        //{
        //    _unitOfWork = unitOfWork;

        //    _gdprDomainRepository = gdprDomainRepository;

        //    var d = new CommandHandler();
        //}

        //public async Task<BusinessResult<int>> Handle(CreateGdprCommand request, CancellationToken cancellationToken)
        //{


        //    var result = new BusinessResult<int>();

        //    var domain = new GdprDomain(request.Gdpr);

        //    var bcErrors = domain.GetBrokenRules();
        //    if (bcErrors.Count > 0)
        //    {
        //        result.AddBrokenRule(bcErrors);
        //        return result;
        //    }

        //    _gdprDomainRepository.Add(domain);

        //    await _unitOfWork.SaveChangesAsync(cancellationToken);

        //    result.Model = domain.Id;

        //    return result;
        //}
    }
}
