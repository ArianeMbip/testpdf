using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

//namespace DossierPortailAPI.Services.Gotenberg.Commands
namespace ApiTestMongo.Gotenberg.Commands
{
    public abstract class BaseCommand
    {
        public Func<Stream, Task> Callback { get; set; }
        public abstract string Route { get; }

        public abstract MultipartFormDataContent GetFormData();
    }
}