//using DossierPortailAPI.Services.Gotenberg.Ressources;
using ApiTestMongo.Gotenberg.Resources;

namespace ApiTestMongo.Gotenberg.Commands
{
    public abstract class BasePdfGotenbergCommand : BaseCommand
    {
        public GotenbergChromiumConvertProperties PdfProperties { get; set; }
    }
}