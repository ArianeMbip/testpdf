namespace ApiTestMongo.Domain.Eleves.Features;

using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Domain.Eleves.Services;
using ApiTestMongo.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateEleve
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly EleveForUpdateDto UpdatedEleveData;

        public Command(Guid id, EleveForUpdateDto updatedEleveData)
        {
            Id = id;
            UpdatedEleveData = updatedEleveData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IEleveRepository _eleveRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IEleveRepository eleveRepository, IUnitOfWork unitOfWork)
        {
            _eleveRepository = eleveRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var eleveToUpdate = await _eleveRepository.GetById(request.Id, cancellationToken: cancellationToken);

            eleveToUpdate.Update(request.UpdatedEleveData);
            _eleveRepository.Update(eleveToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}