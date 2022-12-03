using System;
namespace Hankies.Domain.HankyCode.Flag
{
	public interface IDonned
	{
        /// <summary>
        /// The ID generated from this flags worn visual description.
        /// </summary>
        public Guid DonnedID { get; set; }

        /// <summary>
        /// The location / side this flag is being worn on. Left/Right
        /// </summary>
        public FlaggableLocations Location { get; set; }
    }
}

