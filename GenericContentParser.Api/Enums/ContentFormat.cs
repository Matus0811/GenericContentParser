using System.Text.Json.Serialization;

namespace GenericContentParser.Api.Enums;

public enum ContentFormat
{
    Unknown,

    [JsonStringEnumMemberName("CSV")]
    Csv,
    
    [JsonStringEnumMemberName("INTERNAL_JSON")]
    InternalJson   
}