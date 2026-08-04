# Generic Content Parser API

Generic Content Parser API is an ASP.NET Core Web API that accepts Base64-encoded content in CSV or internal JSON format, parses the decoded data, and returns the result in a unified JSON structure.

## Requirements

- .NET 10 SDK

The task requires .NET 8 or newer. This project was created using .NET 10.

## Running the application

Clone the repository and open its main directory.

Restore the required packages:

```bash
dotnet restore
```

Run the API:

```bash
dotnet run --project GenericContentParser.Api
```

After starting the application, the terminal will display the available HTTP and HTTPS addresses.
The Scalar API documentation is available in the development environment at:

```text
https://localhost:<port>/scalar/v1
```
Replace `<port>` with the port displayed in the terminal.

## Endpoint

**POST** `/api/v1/parse-content`  
**Content-Type:** `application/json`

### Request format

```json
{
  "type": "CSV",
  "content": "Base64-encoded content"
}
```

Supported values of `type`:
- `CSV`
- `INTERNAL_JSON`

The `content` property must contain UTF-8 text encoded with Base64.

### CSV format

The first CSV row must contain column headers. Each following row is converted into an object whose property names come from the header.

Example decoded CSV content:
```csv
id,name,city
1,Kamil,Olsztyn
2,Anna,Warszawa
```

Example request:
```json
{
  "type": "CSV",
  "content": "aWQsbmFtZSxjaXR5CjEsS2FtaWwsT2xzenR5bgoyLEFubmEsV2Fyc3phd2E="
}
```

Example response:
```json
{
  "success": true,
  "type": "CSV",
  "processedCount": 2,
  "data": [
    {
      "id": "1",
      "name": "Kamil",
      "city": "Olsztyn"
    },
    {
      "id": "2",
      "name": "Anna",
      "city": "Warszawa"
    }
  ]
}
```

The CSV parser supports quoted fields containing commas, for example:
```csv
id,name,description
1,Kamil,"Backend developer, C# learner"
```

### INTERNAL_JSON format

The decoded `INTERNAL_JSON` content must contain a JSON array of objects.

Example decoded content:
```json
[
  {
    "id": 1,
    "name": "Kamil",
    "isActive": true
  },
  {
    "id": 2,
    "name": "Anna",
    "isActive": false
  }
]
```

Example request:
```json
{
  "type": "INTERNAL_JSON",
  "content": "W3siaWQiOjEsIm5hbWUiOiJLYW1pbCIsImlzQWN0aXZlIjp0cnVlfSx7ImlkIjoyLCJuYW1lIjoiQW5uYSIsImlzQWN0aXZlIjpmYWxzZX1d"
}
```

Example response:
```json
{
  "success": true,
  "type": "INTERNAL_JSON",
  "processedCount": 2,
  "data": [
    {
      "id": 1,
      "name": "Kamil",
      "isActive": true
    },
    {
      "id": 2,
      "name": "Anna",
      "isActive": false
    }
  ]
}
```

A single JSON object is not accepted. The decoded JSON must use an array as its root element.

## Error responses

The API returns `400 Bad Request` when:
- the content type is unsupported,
- the Base64 value is invalid,
- the decoded content is empty,
- the internal JSON is invalid,
- the internal JSON is not an array of objects,
- the CSV content is invalid.

Example error response:
```json
{
  "error": "Content is not valid Base64."
}
```

Requests using a content type other than `Content-Type: application/json` are rejected with `415 Unsupported Media Type`.

## Technologies

- C#
- .NET 10
- ASP.NET Core Web API
- System.Text.Json
- CsvHelper
- Scalar
- OpenAPI