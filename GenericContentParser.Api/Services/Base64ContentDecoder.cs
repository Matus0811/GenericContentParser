using System.Text;

namespace GenericContentParser.Api.Services;

public class Base64ContentDecoder : IContentDecoder
{
    public string DecodeBase64(string content)
    {
        byte[] bytes = Convert.FromBase64String(content);
        return Encoding.UTF8.GetString(bytes);
    }
}