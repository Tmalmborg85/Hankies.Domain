using System;
namespace Hankies.Domain.HankyCode.Flag
{
	public class RecomendedFlag : BaseFlag
	{
		//this seems stupid
		public bool Approved { get; set; }

		public RecomendedFlag(BaseFlag flag) : base(flag)
		{
		}
	}
}

