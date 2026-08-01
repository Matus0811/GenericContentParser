using Microsoft.AspNetCore.Mvc;
using GenericContentParser.Api.DTOs;
using GenericContentParser.Api.Services;
using GenericContentParser.Api.Parsers;
using GenericContentParser.Api.Enums;
using System.Text.Json;

namespace GenericContentParser.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class ContentParserController : ControllerBase
{
    private readonly IContentDecoder _contentDecoder;
    private readonly InternalJsonContentParser _internalJsonContentParser;

    public ContentParserController(IContentDecoder contentDecoder, InternalJsonContentParser internalJsonContentParser)
    {
        _contentDecoder = contentDecoder;
        _internalJsonContentParser = internalJsonContentParser;
    }

    [HttpPost("parse-content")]
    [Consumes("application/json")]
    public ActionResult ParseContent(ParseContentRequest request)
    {
        try
        {
            string decodedContent = _contentDecoder.DecodeBase64(request.Content);

            if (request.Type == ContentFormat.InternalJson)
            {
                var records = _internalJsonContentParser.Parse(decodedContent);

                return Ok(new
                {
                    type = request.Type,
                    processedCount = records.Count,
                    data = records
                });
            }

            return Ok(new
            {
                type = request.Type, 
                decodedContent
            });

        }
        catch(FormatException)
        {
            return BadRequest(new
            {
                error = "Content is not valid Base64."
            });
        }
        catch(JsonException)
        {
            return BadRequest(new
            {
                error = "Content is not valid JSON."
            });
        }
        catch(ArgumentException exception)
        {
            return BadRequest(new
            {
               error = exception.Message 
            });
        }
    }
}