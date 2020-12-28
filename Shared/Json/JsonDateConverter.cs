using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Shared.Json
{
    public class JsonDateConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return DateTime.MinValue;
            else if (DateTime.TryParse(reader.Value.ToString(), out DateTime value))
                return value;

            throw new FormatException();
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("yyyy/MM/dd"));
        }
    }

    public class JsonDateTimeConverter : JsonDateConverter
    {
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            if (value == DateTime.MinValue)
                writer.WriteValue(string.Empty);
            else
                writer.WriteValue(value.ToString("yyyy/MM/dd HH:mm:ss"));
        }
    }
}
