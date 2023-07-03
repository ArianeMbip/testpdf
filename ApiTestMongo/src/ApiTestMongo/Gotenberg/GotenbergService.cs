using Newtonsoft.Json;
//using DossierPortailAPI.Services.Gotenberg.Commands;
using ApiTestMongo.Gotenberg.Commands;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System;

//namespace DossierPortailAPI.Services.Gotenberg
namespace ApiTestMongo.Gotenberg
{
    public class GotenbergService : IGotenbergService
    {
        private readonly ILogger<GotenbergService> _logger;
        private readonly HttpClient _httpClient;

        public GotenbergService(ILogger<GotenbergService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("gotenberg");
        }

        /// <summary>
        /// Convert html to pdf
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task ConvertHtmlToPdf(ConvertHtmlToPdfCommand command)
        {
            try
            {
                await ExecuteCommand(command);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e, "Un des fichiers que vous avec fourni est introuvable. Payload {0}", JsonConvert.SerializeObject(command));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Une erreur inconnu est survenue");
                throw new Exception("Une erreur inconnu est survenue", e);
            }
        }

        /// <summary>
        /// Convert markdown to pdf
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task ConvertMarkdownToPdf(ConvertMarkdownTopdfCommand command)
        {
            try
            {
                await ExecuteCommand(command);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e, "Un des fichiers que vous avec fourni est introuvable. Payload {0}", JsonConvert.SerializeObject(command));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Une erreur inconnu est survenue");
                throw new Exception("Une erreur inconnu est survenue", e);
            }
        }

        /// <summary>
        /// Convert Office to Pdf
        /// </summary>
        /// <returns></returns>
        public async Task ConvertOfficeToPdf(ConvertOfficeToPdfCommand command)
        {
            try
            {
                await ExecuteCommand(command);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e, "Un des fichiers que vous avec fourni est introuvable. Payload {0}", JsonConvert.SerializeObject(command));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Une erreur inconnu est survenue");
                throw new Exception("Une erreur inconnu est survenue", e);
            }
        }

        /// <summary>
        /// This route accepts a form field url with the URL of the page you want to convert to PDF.
        /// </summary>
        /// <param name="command">The command to launch the conversion of the url to Pdf</param>
        /// <returns></returns>
        public async Task ConvertUrlToPdf(ConvertUrlToPdfCommand command)
        {
            try
            {
                await ExecuteCommand(command);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e, "Un des fichiers que vous avec fourni est introuvable. Payload {0}", JsonConvert.SerializeObject(command));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Une erreur inconnu est survenue");
                throw new Exception("Une erreur inconnu est survenue", e);
            }
        }

        #region Private Methods

        private async Task ExecuteCommand<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(command.Route, UriKind.Relative))
            {
                Content = command.GetFormData()
            };
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            await command.Callback(stream);
        }

        #endregion Private Methods
    }
}