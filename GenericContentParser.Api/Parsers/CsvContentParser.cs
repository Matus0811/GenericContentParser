using System.Globalization;
using CsvHelper;

namespace GenericContentParser.Api.Parsers;

public class CsvContentParser
{
    public List<Dictionary<string, string?>> Parse(string content)
    {
        List<Dictionary<string, string?>> records = new();

        using StringReader reader = new(content);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();

        string[] headers = csv.HeaderRecord ?? throw new ArgumentException("CSV header is missing");

        while(csv.Read())
        {
            Dictionary<string,string?> record = new();

            foreach (string header in headers)
            {
                record[header] = csv.GetField(header);
            }

            records.Add(record);
        }

        return records;
    }
}