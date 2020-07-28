using System;
using System.Threading;
using System.Threading.Tasks;
using barefoot.finances.service.core.interfaces;
using barefoot.finances.service.models;
using FluentValidation;
using MediatR;

namespace barefoot.finances.service.core.gethouseholdinfo
{
    public class GetHouseholdInfoCommand : IRequest<HouseholdInfo>
    {
        public string UserId { get; set; }
    }
    
    public class GetHouseholdInfoValidator : AbstractValidator<GetHouseholdInfoCommand>
    {
        public GetHouseholdInfoValidator()
        {
            RuleFor(m => m.UserId)
                .NotNull()
                .WithMessage("User ID must be specified");
        }
    }

    public class GetHouseholdInfoHandler : IRequestHandler<GetHouseholdInfoCommand, HouseholdInfo>
    {
        private readonly IDataPersistance _dataClient;

        public GetHouseholdInfoHandler(IDataPersistance db)
        {
            _dataClient = db ?? throw new ArgumentNullException(nameof(db));    
        }

        public async Task<HouseholdInfo> Handle(GetHouseholdInfoCommand request, CancellationToken cancellationToken)
        {
            return await _dataClient.GetHouseholdInfoAsync(request.UserId);
        }
    }
}