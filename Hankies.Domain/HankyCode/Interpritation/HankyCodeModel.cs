using System;
using Hankies.Domain.HankyCode.Flag;
using System.Collections.Generic;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Hankies.Domain.HankyCode.Interpritation
{
	/// <summary>
	/// A basic model of the Hanky Code used to save and load the code as a JSON. 
	/// </summary>
	public class HankyCodeModel
	{
		public Decimal Version { get; set; }

        [JsonConverter(typeof(DictionaryGUIDConverter))]
        public Dictionary<Guid, BaseFlag> Flags { get; set; }

        [JsonConverter(typeof(DictionaryGUIDConverter))]
        public Dictionary<Guid, RecomendedFlag> RecomendedFlags { get; set; }
        

		public HankyCodeModel()
		{
            Version = 0;
            RecomendedFlags = new Dictionary<Guid, RecomendedFlag>();
            Flags = new Dictionary<Guid, BaseFlag>();
        }
	}

    public static class HankyCodeModelExtensions
    {
        public static string ToJSON(this HankyCodeModel model)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            //return JsonSerializer.Serialize(model, options);
            return JsonSerializer.Serialize(model);
        }
    }
}

