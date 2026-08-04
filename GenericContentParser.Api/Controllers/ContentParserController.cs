using Microsoft.AspNetCore.Mvc;
using GenericContentParser.Api.DTOs;
using GenericContentParser.Api.Services;
using GenericContentParser.Api.Parsers;
using GenericContentParser.Api.Enums;
using System.Text.Json;
using CsvHelper;

namespace GenericContentParser.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class ContentParserController : ControllerBase
{
    private readonly IContentDecoder _contentDecoder;
    private readonly InternalJsonContentParser _internalJsonContentParser;
    private readonly CsvContentParser _csvContentParser;

    public ContentParserController(IContentDecoder contentDecoder, InternalJsonContentParser internalJsonContentParser, CsvContentParser csvContentParser)
    {
        _contentDecoder = contentDecoder;
        _internalJsonContentParser = internalJsonContentParser;
        _csvContentParser = csvContentParser;
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

                ParseContentResponse response = new()
                {
                  Success = true,
                  Type = request.Type,
                  ProcessedCount = records.Count,
                  Data = records  
                };
                
                return Ok(response);
            }

            if(request.Type == ContentFormat.Csv)
            {
                var records = _csvContentParser.Parse(decodedContent);

                ParseContentResponse response = new()
                {
                  Success = true,
                  Type = request.Type,
                  ProcessedCount = records.Count,
                  Data = records  
                };
                
                return Ok(response);
            }

            return BadRequest(new
            {
                error = "Unsupported content type."
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
        catch(CsvHelperException)
        {
            return BadRequest(new
            {
               error = "Content is not valid CSV." 
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