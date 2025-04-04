namespace API.Converters;

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TimeOnlyConverter : JsonConverter<TimeOnly>
{
    private const string Format = "HH:mm";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var timeString = reader.GetString();
        if (string.IsNullOrWhiteSpace(timeString))
        {
            throw new JsonException("Invalid time format. Expected 'HH:mm'.");
        }

        if (TimeOnly.TryParseExact(timeString, Format, null, DateTimeStyles.None, out var result))
        {
            return result;
        }

        throw new JsonException($"Invalid time format: '{timeString}'. Expected 'HH:mm'.");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}