namespace ApiTestMongo.Domain.Eleves.DomainEvents;

public sealed class EleveCreated : DomainEvent
{
    public Eleve Eleve { get; set; } 
}
            