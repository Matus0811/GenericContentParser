using System.ComponentModel.DataAnnotations;
using GenericContentParser.Api.Enums;

namespace GenericContentParser.Api.DTOs;

public class ParseContentRequest
{
    [Required]
    public required ContentFormat Type { get; set; }
    
    [Required]
    public required string Content { get; set; } 
}