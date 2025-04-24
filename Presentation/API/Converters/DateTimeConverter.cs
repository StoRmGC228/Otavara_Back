namespace API.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //return DateTime.ParseExact(reader.GetString(), "yyyy-MM-dd HH:mm", null);
        //return DateTime.ParseExact(reader.GetString(), "yyyy-MM-dd HH:mm", null).ToUniversalTime();
        var parsed = DateTime.ParseExact(reader.GetString(), "yyyy-MM-dd HH:mm", null);
        return DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm"));
    }
}