using System;

namespace UMT
{
	/// <summary>
	/// Exponential distribution.
	/// FROM http://stackoverflow.com/questions/2106503/pseudorandom-number-generator-exponential-distribution
	/// </summary>
	public static class ExponentialDistribution
	{
		public static float Normalize( float randx, float lambda )
		{
			return Convert.ToSingle((Math.Log(1-randx) / (-lambda)));
		}
	}
}