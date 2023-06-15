namespace ApiTestMongo.Domain.Eleves.DomainEvents;

public sealed class EleveUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            