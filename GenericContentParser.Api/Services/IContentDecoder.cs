namespace GenericContentParser.Api.Services;

public interface IContentDecoder
{
    string DecodeBase64(string content);
}