namespace ApiTestMongo.Domain.Eleves.Features;

using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Domain.Eleves.Services;
using ApiTestMongo.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetEleveList
{
    public sealed class Query : IRequest<PagedList<EleveDto>>
    {
        public readonly EleveParametersDto QueryParameters;

        public Query(EleveParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<EleveDto>>
    {
        private readonly IEleveRepository _eleveRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IEleveRepository eleveRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _eleveRepository = eleveRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<EleveDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _eleveRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<EleveDto>();

            return await PagedList<EleveDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}