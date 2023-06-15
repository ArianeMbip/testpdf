namespace ApiTestMongo.Domain.Eleves.Features;

using ApiTestMongo.Domain.Eleves.Services;
using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddEleve
{
    public sealed class Command : IRequest<EleveDto>
    {
        public readonly EleveForCreationDto EleveToAdd;

        public Command(EleveForCreationDto eleveToAdd)
        {
            EleveToAdd = eleveToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, EleveDto>
    {
        private readonly IEleveRepository _eleveRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IEleveRepository eleveRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _eleveRepository = eleveRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EleveDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var eleve = Eleve.Create(request.EleveToAdd);
            await _eleveRepository.Add(eleve, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var eleveAdded = await _eleveRepository.GetById(eleve.Id, cancellationToken: cancellationToken);
            return _mapper.Map<EleveDto>(eleveAdded);
        }
    }
}