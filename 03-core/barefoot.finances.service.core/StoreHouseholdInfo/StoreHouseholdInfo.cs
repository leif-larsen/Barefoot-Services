using System;
using System.Threading;
using System.Threading.Tasks;
using barefoot.finances.service.core.interfaces;
using barefoot.finances.service.models;
using FluentValidation;
using MediatR;

namespace barefoot.finances.service.core.storehouseholdinfo
{
    public class StoreHouseholdInfoCommand : IRequest<bool>
    {
        public HouseholdInfo Info { get; set; }
    }
    
    public class StoreHouseholdInfoValidator : AbstractValidator<StoreHouseholdInfoCommand>
    {
        public StoreHouseholdInfoValidator()
        {
            RuleFor(m => m.Info)
                .NotNull()
                .WithMessage("Household information must be specified");
        }
    }

    public class StoreHouseholdInfoHandler : IRequestHandler<StoreHouseholdInfoCommand, bool>
    {
        private readonly IDataPersistance _dataClient;

        public StoreHouseholdInfoHandler(IDataPersistance db)
        {
            _dataClient = db ?? throw new ArgumentNullException(nameof(db));    
        }

        public async Task<bool> Handle(StoreHouseholdInfoCommand request, CancellationToken cancellationToken)
        {
            return await _dataClient.SaveHouseholdInfoAsync(request.Info);
        }
    }
}