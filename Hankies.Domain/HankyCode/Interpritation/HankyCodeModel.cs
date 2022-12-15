using System;
using Hankies.Domain.HankyCode.Flag;
using System.Collections.Generic;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Reflection;
using System.IO;

namespace Hankies.Domain.HankyCode.Interpritation
{
	/// <summary>
	/// A basic model of the Hanky Code used to save and load the code as a JSON. 
	/// </summary>
	public class HankyCodeModel
	{
		public Decimal Version { get; set; }

        public List<FlagJSONModel> Flags { get; set; }

        //[JsonConverter(typeof(DictionaryGUIDConverter))]
        //public Dictionary<Guid, RecomendedFlag> RecomendedFlags { get; set; }



        public HankyCodeModel()
		{
           Version = 0;
           //// RecomendedFlags = new Dictionary<Guid, RecomendedFlag>();
           Flags = new List<FlagJSONModel>();
        }

        //maybe this should be in some kind of file manager service?
        public static HankyCodeModel LoadHankyCodeModel(string fileName = "HankyCode.JSON")
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                fileName);
            var hankyCodeJSON = File.ReadAllText(path);
            return JsonSerializer.Deserialize<HankyCodeModel>(hankyCodeJSON);
        }


    }
}

