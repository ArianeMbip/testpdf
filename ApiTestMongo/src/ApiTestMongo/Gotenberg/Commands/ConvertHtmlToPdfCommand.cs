using System.IO;
using System.Linq;
using System.Net.Http;

//namespace DossierPortailAPI.Services.Gotenberg.Commands
namespace ApiTestMongo.Gotenberg.Commands
{
    public class ConvertHtmlToPdfCommand : BasePdfGotenbergCommand
    {
        public override string Route => "/forms/chromium/convert/html";
        public string PathToHtmlFile { get; set; }

        public override MultipartFormDataContent GetFormData()
        {
            var stream = File.OpenRead(PathToHtmlFile);

            var result = new MultipartFormDataContent {
                {new StreamContent(stream), "files", "index.html"},
            };

            foreach (var content in PdfProperties.GetFormData())
            {
                result.Add(content);
            }
            
            return result;
        }
    }
}