namespace ApiTestMongo.Domain.Eleves.Features;

using ApiTestMongo.Domain.Eleves.Services;
using ApiTestMongo.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteEleve
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
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
            var recordToDelete = await _eleveRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _eleveRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}