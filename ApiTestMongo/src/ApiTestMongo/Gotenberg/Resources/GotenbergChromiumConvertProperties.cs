//using DossierPortailAPI.Common.Extensions;
using ApiTestMongo.Extensions;
using ApiTestMongo.Extensions.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;

//namespace DossierPortailAPI.Services.Gotenberg.Ressources
namespace ApiTestMongo.Gotenberg.Resources
{
    public class GotenbergChromiumConvertProperties
    {
        /// <summary>
        /// The properties of the result pdf
        /// </summary>
        public PaperProperties PaperProperties { get; set; }

        /// <summary>
        /// Duration to wait when loading an HTML document before converting it to PDF
        /// </summary>
        public int WaitDelay { get; set; }

        /// <summary>
        /// The window.status value to wait for before converting an HTML document to PDF
        /// </summary>
        public string WaitWindowStatut { get; set; }

        /// <summary>
        ///  HTTP headers to send by Chromium while loading the HTML document (JSON format)
        /// </summary>
        public Dictionary<string, string> ExtraHttpHeaders { get; set; }

        /// <summary>
        /// Page ranges to print, e.g., '1-5, 8, 11-13' - empty means all pages
        /// </summary>
        public string NativePageRanges { get; set; }

        /// <summary>
        /// The PDF format of the resulting PDF
        /// </summary>
        public PdfFormatEnum PdfFormat { get; set; }

        /// <summary>
        /// List of Assets (fonts, stylesheets, images)
        /// </summary>
        public Dictionary<string, string> Assets { get; set; }

        public GotenbergChromiumConvertProperties()
        {
            PaperProperties = new PaperProperties();
            WaitDelay = 0;
            WaitWindowStatut = "ready";
            ExtraHttpHeaders = new Dictionary<string, string>();
            NativePageRanges = "";
            PdfFormat = PdfFormatEnum.PdfA1a;
            Assets = new Dictionary<string, string>();
        }

        public MultipartFormDataContent GetFormData()
        {
            var excludes = new List<string> { "PaperSize", "PaperWidth", "PaperHeight" };
            var result = new MultipartFormDataContent();

            // Handle PaperProperties
            foreach (var property in PaperProperties.GetType().GetProperties())
            {
                if (!excludes.Contains(property.Name))
                {
                    result.Add(new StringContent(property.GetValue(PaperProperties, null) as string ?? string.Empty, Encoding.UTF8), property.Name.ToLowerFirstChar());
                }
            }
            if (PaperProperties.PaperSize == PaperSizeEnum.Custom)
            {
                result.Add(new StringContent(PaperProperties.PaperWidth.ToString(CultureInfo.InvariantCulture), Encoding.UTF8), "paperWidth");
                result.Add(new StringContent(PaperProperties.PaperHeight.ToString(CultureInfo.InvariantCulture), Encoding.UTF8), "paperHeight");
            }
            else
            {
                var (width, height) = PaperProperties.PaperSize.GetSize();
                result.Add(new StringContent(width, Encoding.UTF8), "paperWidth");
                result.Add(new StringContent(height, Encoding.UTF8), "paperHeight");
            }

            if (WaitDelay > 0 && string.IsNullOrEmpty(WaitWindowStatut))
            {
                result.Add(new StringContent(WaitDelay.ToString()), "waitDelay");
            }
            result.Add(new StringContent(WaitWindowStatut ?? string.Empty, Encoding.UTF8), "waitWindowStatut");
            result.Add(new StringContent(NativePageRanges, Encoding.UTF8), "nativePageRanges");
            result.Add(new StringContent(PdfFormat.GetDescription(), Encoding.UTF8), "PdfFormat");
            if (ExtraHttpHeaders.Count > 0) result.Add(new StringContent(JsonConvert.SerializeObject(ExtraHttpHeaders)), "extraHttpHeaders");
            foreach (var (key, value) in Assets)
            {
                var stream = File.OpenRead(value);
                result.Add(new StreamContent(stream), "files", key);
            }

            return result;
        }
    }
}