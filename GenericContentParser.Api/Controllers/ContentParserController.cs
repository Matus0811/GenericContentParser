using Microsoft.AspNetCore.Mvc;
using GenericContentParser.Api.DTOs;

namespace GenericContentParser.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class ContentParserController : ControllerBase
{
    [HttpPost("parse-content")]
    public ActionResult ParseContent(ParseContentRequest request)
    {
        return Ok(request);
    }
}