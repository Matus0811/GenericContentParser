using System.Text.Json;

namespace GenericContentParser.Api.Parsers;

public class InternalJsonContentParser
{
    public List<Dictionary<string, JsonElement>> Parse(string content)
    {
        return JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(content) ?? 
        throw new ArgumentException("JSON content cannot be null.");
    }
}