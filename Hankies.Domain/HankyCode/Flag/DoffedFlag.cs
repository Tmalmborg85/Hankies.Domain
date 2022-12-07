using System;
namespace Hankies.Domain.HankyCode.Flag
{
	public class DoffedFlag : BaseFlag
	{
		public DoffedFlag(BaseFlag flag) : base(flag)
		{
		}

        /// <summary>
        /// Create a donned flag from this flag. 
        /// </summary>
        /// <param name="location">The side the flag is going to be worn on</param>
        /// <returns>A <c>DonnedFlag</c> or Null if an error happens.</returns>
        public DonnedFlag DonnFlag(FlaggableLocations location)
        {
            try
            {
                return new DonnedFlag(this, location);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

