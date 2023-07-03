namespace ApiTestMongo.Controllers.v1;

using ApiTestMongo.Domain.Eleves.Features;
using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using ApiTestMongo.Gotenberg.Commands;
using ApiTestMongo.Gotenberg.Resources;
using ApiTestMongo.Gotenberg;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using MimeKit;

[ApiController]
[Route("api/eleves")]
[ApiVersion("1.0")]
public sealed class ElevesController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IGotenbergService _gotenbergService;
    private readonly ILogger<ElevesController> _logger;
    

    public ElevesController(IMediator mediator, IGotenbergService gotenbergService, ILogger<ElevesController> logger)
    {
        _mediator = mediator;
        _gotenbergService = gotenbergService;
        _logger = logger;
    }
    

    /// <summary>
    /// Creates a new Eleve record.
    /// </summary>
    /// <response code="201">Eleve created.</response>
    /// <response code="400">Eleve has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Eleve.</response>
    [ProducesResponseType(typeof(EleveDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddEleve")]
    public async Task<ActionResult<EleveDto>> AddEleve([FromBody]EleveForCreationDto eleveForCreation)
    {
        var command = new AddEleve.Command(eleveForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetEleve",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Eleve by ID.
    /// </summary>
    /// <response code="200">Eleve record returned successfully.</response>
    /// <response code="400">Eleve has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Eleve.</response>
    [ProducesResponseType(typeof(EleveDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetEleve")]
    public async Task<ActionResult<EleveDto>> GetEleve(Guid id)
    {
        var query = new GetEleve.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Eleves.
    /// </summary>
    /// <response code="200">Eleve list returned successfully.</response>
    /// <response code="400">Eleve has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Eleve.</response>
    /// <remarks>
    /// Requests can be narrowed down with a variety of query string values:
    /// ## Query String Parameters
    /// - **PageNumber**: An integer value that designates the page of records that should be returned.
    /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
    /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
    /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
    ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
    ///     - {Operator} is one of the Operators below
    ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
    ///
    ///    | Operator | Meaning                       | Operator  | Meaning                                      |
    ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
    ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
    ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
    ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
    ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
    ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
    ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
    ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
    ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<EleveDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetEleves")]
    public async Task<IActionResult> GetEleves([FromQuery] EleveParametersDto eleveParametersDto)
    {
        var query = new GetEleveList.Query(eleveParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Deletes an existing Eleve record.
    /// </summary>
    /// <response code="204">Eleve deleted.</response>
    /// <response code="400">Eleve has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Eleve.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteEleve")]
    public async Task<ActionResult> DeleteEleve(Guid id)
    {
        var command = new DeleteEleve.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Updates an entire existing Eleve.
    /// </summary>
    /// <response code="204">Eleve updated.</response>
    /// <response code="400">Eleve has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Eleve.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateEleve")]
    public async Task<IActionResult> UpdateEleve(Guid id, EleveForUpdateDto eleve)
    {
        var command = new UpdateEleve.Command(id, eleve);
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpGet("pdf", Name = "Download")]
    public async Task<IActionResult> Download()
    {
        _logger.LogInformation("pdfinfo");
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Pdf", "htmlpage.html");
        var builder = new BodyBuilder();
        using (StreamReader reader = System.IO.File.OpenText(directoryPath))
        {
            builder.HtmlBody = reader.ReadToEnd();
        }
        var dossierFile = string.Format(builder.HtmlBody //dossier.CompanyName, dossier.SectorOfActivity, dossier.PotentialRevenue,
            //dossier.PotentialVolume, dossier.NatureOfActivities, dossier.NatureOfOMActivities, dossier.LegalRepresentativeName,
            //string.Join(", ", dossier.BeneficialOwners.ToList().ConvertAll(o => o.BeneficialOwnerName)), dossier.CompanyType,
            //dossier.OthersComplementaryInformation, dossier.Service.Name
            );

        var tempFile = Path.GetTempFileName();
        await System.IO.File.WriteAllTextAsync(tempFile, dossierFile, default);
        Stream pdfStream = null;
        await _gotenbergService.ConvertHtmlToPdf(new ConvertHtmlToPdfCommand
        {
            Callback = async stream => pdfStream = stream,
            PathToHtmlFile = tempFile,
            PdfProperties = new GotenbergChromiumConvertProperties()
        });
        return File(pdfStream, "application/pdf");
    }

    // endpoint marker - do not delete this comment
}
