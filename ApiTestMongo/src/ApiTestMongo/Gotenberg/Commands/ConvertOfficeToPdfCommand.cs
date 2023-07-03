
//using DossierPortailAPI.Common.Extensions;
using ApiTestMongo.Extensions;
//using DossierPortailAPI.Services.Gotenberg.Ressources;
using ApiTestMongo.Gotenberg.Resources;
using ApiTestMongo.Gotenberg.Commands;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using ApiTestMongo.Extensions.Services;


//namespace DossierPortailAPI.Services.Gotenberg.Commands
namespace ApiTestMongo.Gotenberg.Commands
{
    public class ConvertOfficeToPdfCommand : BaseCommand
    {
        public override string Route => "/forms/libreoffice/convert";

        public Dictionary<string, string> PathsToHtml { get; set; }
        public string NativePageRange { get; set; }
        public PdfFormatEnum PdfFormat { get; set; }
        public bool Merge { get; set; }
        public bool Landscape { get; set; }

        public ConvertOfficeToPdfCommand()
        {
            PathsToHtml = new Dictionary<string, string>();
            NativePageRange = "";
            PdfFormat = PdfFormatEnum.PdfA1a;
            Merge = false;
            Landscape = false;
        }

        public ConvertOfficeToPdfCommand(Dictionary<string, string> pathsToHtml, string nativePageRange, PdfFormatEnum pdfFormat, bool merge, bool landScape)
        {
            PathsToHtml = pathsToHtml;
            NativePageRange = nativePageRange;
            PdfFormat = pdfFormat;
            Merge = merge;
            Landscape = landScape;
        }

        public override MultipartFormDataContent GetFormData()
        {
            var result = new MultipartFormDataContent() {
                {new StringContent(NativePageRange, Encoding.UTF8), "nativePageRange" },
                {new StringContent(Merge.ToString(), Encoding.UTF8), "merge" },
                {new StringContent(Landscape.ToString(), Encoding.UTF8), "landscape" },
                {new StringContent(PdfFormat.GetDescription(), Encoding.UTF8), "pdfFormat" }
            };

            foreach (var (key, value) in PathsToHtml)
            {
                using var stream = File.OpenRead(value);
                result.Add(new StreamContent(stream), "files", key);
            }

            return result;
        }
    }
}