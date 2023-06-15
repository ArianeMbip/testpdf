namespace ApiTestMongo.Domain.Eleves;

using SharedKernel.Exceptions;
using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Domain.Eleves.Validators;
using ApiTestMongo.Domain.Eleves.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


public class Eleve : BaseEntity
{
    [Column("Name")]
    public virtual string Nom { get; private set; }

    public virtual string Note { get; private set; }


    public static Eleve Create(EleveForCreationDto eleveForCreationDto)
    {
        new EleveForCreationDtoValidator().ValidateAndThrow(eleveForCreationDto);

        var newEleve = new Eleve();

        newEleve.Nom = eleveForCreationDto.Nom;
        newEleve.Note = eleveForCreationDto.Note;

        newEleve.QueueDomainEvent(new EleveCreated(){ Eleve = newEleve });
        
        return newEleve;
    }

    public Eleve Update(EleveForUpdateDto eleveForUpdateDto)
    {
        new EleveForUpdateDtoValidator().ValidateAndThrow(eleveForUpdateDto);

        Nom = eleveForUpdateDto.Nom;
        Note = eleveForUpdateDto.Note;

        QueueDomainEvent(new EleveUpdated(){ Id = Id });
        return this;
    }
    
    protected Eleve() { } // For EF + Mocking
}