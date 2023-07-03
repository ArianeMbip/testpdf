//using DossierPortailAPI.Services.Gotenberg.Commands;
using ApiTestMongo.Gotenberg.Commands;
using System.Threading.Tasks;

//namespace DossierPortailAPI.Services.Gotenberg
namespace ApiTestMongo.Gotenberg
{
    public interface IGotenbergService
    {
        /// <summary>
        /// Convert an url to pdf
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task ConvertUrlToPdf(ConvertUrlToPdfCommand command);

        /// <summary>
        /// Convert html to pdf
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task ConvertHtmlToPdf(ConvertHtmlToPdfCommand command);

        /// <summary>
        /// Convert markdown to pdf
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task ConvertMarkdownToPdf(ConvertMarkdownTopdfCommand command);

        /// <summary>
        /// Convert Office to Pdf
        /// </summary>
        /// <returns></returns>
        Task ConvertOfficeToPdf(ConvertOfficeToPdfCommand command);
    }
}