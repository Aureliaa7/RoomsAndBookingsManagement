using Newtonsoft.Json;
using System.Globalization;

namespace HRAAB_Management.Business.Convertors
{
    internal class DateOnlyNewtonsoftConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyyMMdd";

        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string str = reader.Value?.ToString() ?? throw new JsonSerializationException("Expected date string.");
            return DateOnly.ParseExact(str, Format, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
