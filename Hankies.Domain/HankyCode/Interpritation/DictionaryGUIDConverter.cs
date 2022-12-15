using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hankies.Domain.HankyCode.Flag;

namespace Hankies.Domain.HankyCode.Interpritation
{
	public class DictionaryGUIDConverter : JsonConverter<Dictionary<Guid, BaseFlag>>
    {
        public DictionaryGUIDConverter()
		{
		}

        public override Dictionary<Guid, BaseFlag> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"JsonTokenType was of type {reader.TokenType}, only objects are supported");
            }

            var dictionary = new Dictionary<Guid, BaseFlag>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("JsonTokenType was not PropertyName");
                }

                var propertyName = reader.GetString();

                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new JsonException("Failed to get property name");
                }

                reader.Read();

                string itemValue = reader.GetString();

                Guid key;
                Guid.TryParse(propertyName, out key);
                //dictionary.Add(key, itemValue);
            }

            return dictionary;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<Guid, BaseFlag> dictionary, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach ((Guid key, object value) in dictionary)
            {
                var propertyName = key.ToString();
                writer.WriteString(propertyName, value.ToString());
            }

            writer.WriteEndObject();
        }

 
    }
}

