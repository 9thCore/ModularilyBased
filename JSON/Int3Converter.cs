using Newtonsoft.Json;
using System;

namespace ModularilyBased.JSON
{
    public class Int3Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Int3);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (Int3)serializer.Deserialize<Int3Json>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Int3 package = (Int3)value;
            serializer.Serialize(writer, (Int3Json)package);
        }
    }

    public record Int3Json(int X, int Y, int Z)
    {
        public static explicit operator Int3(Int3Json i) => new(i.X, i.Y, i.Z);
        public static explicit operator Int3Json(Int3 i) => new(i.x, i.y, i.z);
    }
}
