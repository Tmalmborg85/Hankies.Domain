using System;
using System.Text.Json;
using Hankies.Domain.HankyCode.Appearance;
using Hankies.Domain.HankyCode.Flag;
using Hankies.Domain.HankyCode.Interpritation;

namespace Hankies.Domain.HankyCode
{

	public static class Extensions
	{
        public static bool IsType<T>(this object obj)
        {
            return obj.GetType() == typeof(T);
        }

        public static bool IsSameTypeAs(this object objA, object objB)
        {
            return objA.GetType() == objB.GetType();
        }


        /// <summary>
        /// Checks a string to see if it is well-formated JSON. If a string can
        /// be parsed by <c>System.Test.Json.JsonDocument</c> then it is JSON
        /// </summary>
        /// <remarks>
        /// From Stack overflow https://stackoverflow.com/questions/58629279/validate-if-string-is-valid-json-fastest-way-possible-in-net-core-3-0
        /// </remarks>
        /// <param name="source">the source string</param>
        /// <returns>True or False</returns>
        public static bool IsJson(this string source)
        {
            if (source == null)
                return false;

            try
            {
                JsonDocument.Parse(source);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        /// <summary>
        /// Turn any object into a JSON string. 
        /// </summary>
        /// <param name="source">the object to convert to JSON</param>
        /// <returns>A valid JSON string</returns>
        public static string ToJSON(this object source)
        {
            var result = string.Empty;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            result = JsonSerializer.Serialize(source, options);
            return result;
        }

        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            source = source.ToLower();
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }

        public static FlagJSONModel ToModel(this BaseFlag flag)
        {
            var model = new FlagJSONModel()
            {
                ID = flag.ID,
                Type = flag.GetType().ToString(),
                Trait = flag.Trait,
                VisualDescription = flag.VisualDescription
            };

            if (flag.IsType<CottonHanky>())
            {
                var cottenFlag = (CottonHanky)flag;
                model.Appearance = cottenFlag.Appearance;
            }
            else if (flag.GetType() == typeof(DyedFabricFlag))
            {
                var dyedFabricFlag = (DyedFabricFlag)flag;
                model.Appearance = new SolidColor(dyedFabricFlag.Color);
            }

            Console.WriteLine("Created Flag Model.\n - Type: " + model.Type
                + "\n - Look: " + model.VisualDescription);

            return model;
        }

        

    }
}




