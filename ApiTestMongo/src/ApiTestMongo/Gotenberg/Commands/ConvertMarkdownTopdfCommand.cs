using System.IO;
using System.Linq;
using System.Net.Http;

//namespace DossierPortailAPI.Services.Gotenberg.Commands
namespace ApiTestMongo.Gotenberg.Commands
{
    public class ConvertMarkdownTopdfCommand : BasePdfGotenbergCommand
    {
        public override string Route => "/forms/chromium/convert/markdown";

        public string PathToHtmlFile { get; set; }
        public string PathToMarkdownFile { get; set; }

        public override MultipartFormDataContent GetFormData()
        {
            using var stream = File.OpenRead(PathToHtmlFile);
            using var mdStream = File.OpenRead(PathToMarkdownFile);

            var result = new MultipartFormDataContent {
                {new StreamContent(stream), "files", "index.html"},
                {new StreamContent(mdStream), "files", "file.md"},
            };

            result.Union(PdfProperties.GetFormData());
            return result;
        }
    }
}