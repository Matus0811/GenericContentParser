using System.Text.Json.Serialization;
using GenericContentParser.Api.Parsers;
using GenericContentParser.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IContentDecoder, Base64ContentDecoder>();
builder.Services.AddSingleton<InternalJsonContentParser>();
builder.Services.AddSingleton<CsvContentParser>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: null, allowIntegerValues: false));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();