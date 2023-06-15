namespace ApiTestMongo.Domain.Eleves.Features;

using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Domain.Eleves.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetEleve
{
    public sealed class Query : IRequest<EleveDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, EleveDto>
    {
        private readonly IEleveRepository _eleveRepository;
        private readonly IMapper _mapper;

        public Handler(IEleveRepository eleveRepository, IMapper mapper)
        {
            _mapper = mapper;
            _eleveRepository = eleveRepository;
        }

        public async Task<EleveDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _eleveRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<EleveDto>(result);
        }
    }
}