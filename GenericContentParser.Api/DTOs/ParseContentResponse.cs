using GenericContentParser.Api.Enums;

namespace GenericContentParser.Api.DTOs;  

public class ParseContentResponse
{
    public bool Success {get; set;}
    public ContentFormat Type {get; set;}
    public int ProcessedCount {get; set;}
    public IEnumerable<object> Data {get; set;} = [];
}