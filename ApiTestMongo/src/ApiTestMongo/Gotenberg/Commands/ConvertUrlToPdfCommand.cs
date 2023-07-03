//using DossierPortailAPI.Services.Gotenberg.Ressources;
using ApiTestMongo.Gotenberg.Resources;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

//namespace DossierPortailAPI.Services.Gotenberg.Commands
namespace ApiTestMongo.Gotenberg.Commands
{
    public class ConvertUrlToPdfCommand : BasePdfGotenbergCommand
    {
        public override string Route => "/chromium/convert/url";
        public string Url { get; set; }
        public string PathToHeader { get; set; }
        public string PathToFooter { get; set; }

        public ConvertUrlToPdfCommand()
        {
            PdfProperties = new GotenbergChromiumConvertProperties()
            {
                PaperProperties = new PaperProperties()
                {
                    MarginBottom = 0,
                    MarginLeft = 0,
                    MarginRight = 0,
                    MarginTop = 0
                }
            };
        }

        public ConvertUrlToPdfCommand(string url, Func<Stream, Task> callback, GotenbergChromiumConvertProperties pdfProperties)
        {
            Url = url;
            Callback = callback;
            PdfProperties = pdfProperties ?? new GotenbergChromiumConvertProperties()
            {
                PaperProperties = new PaperProperties()
                {
                    MarginBottom = 0,
                    MarginLeft = 0,
                    MarginRight = 0,
                    MarginTop = 0
                }
            };
        }

        /// <summary>
        /// Get Formdata to send to Gotenberg
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Throw if an asset or a file is not found</exception>
        public override MultipartFormDataContent GetFormData()
        {
            using var headerStream = File.OpenRead(PathToHeader);
            using var footerStream = File.OpenRead(PathToFooter);
            var result = new MultipartFormDataContent {
                {new StringContent(Url), "url"},
                {new StreamContent(headerStream), "files", "header.html"},
                {new StreamContent(footerStream), "files", "footer.html"},
            };

            foreach (var content in PdfProperties.GetFormData())
            {
                result.Add(content);
            }
            return result;
        }
    }
}