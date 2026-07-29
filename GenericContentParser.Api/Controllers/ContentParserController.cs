using Microsoft.AspNetCore.Mvc;
using GenericContentParser.Api.DTOs;
using GenericContentParser.Api.Services;

namespace GenericContentParser.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class ContentParserController : ControllerBase
{
    private readonly IContentDecoder _contentDecoder;

    public ContentParserController(IContentDecoder contentDecoder)
    {
        _contentDecoder = contentDecoder;
    }

    [HttpPost("parse-content")]
    public ActionResult ParseContent(ParseContentRequest request)
    {
        try
        {
            string decodedContent = _contentDecoder.DecodeBase64(request.Content);
            return Ok(new
            {
                type = request.Type, decodedContent
            });
        }
        catch(FormatException)
        {
            return BadRequest(new
            {
                error = "Content is not valid Base64."
            });
        }
    }
}