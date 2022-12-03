using System;
using System.ComponentModel;

namespace Hankies.Domain.HankyCode.Flag
{
	/// <summary>
	/// Happens when there is an error creating the GUID ID for a flag. 
	/// </summary>
	public class FailedToCreateFlagIDException : Exception
	{
		public FailedToCreateFlagIDException(Exception exception, string seed)
			: base ("Failed to create flag ID from seed: " + seed, exception)
		{

		}
	}
}

