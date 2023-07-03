using Refit;
using System.Net.Http;
using System.Threading.Tasks;

//namespace DossierPortailAPI.Services.Gotenberg
namespace ApiTestMongo.Gotenberg
{
    public interface IGotenbergApi
    {
        [Post("/forms/chromium/convert/html")]
        [Multipart]
        Task<HttpResponseMessage> ConvertHtmlToPdf(StreamPart index, StreamPart header = null, StreamPart footer = null,
            double scale = 1, double marginLeft = 0.39, double marginRight = 0.39, double marginBottom = 0.39,
            double marginTop = 0.39, double paperWidth = 8.5, double paperHeight = 1);
    }
}