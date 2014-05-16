using System;
using UnityEngine;

namespace UMT
{
	/// <summary>
	/// Random square.
	/// </summary>
	public static class RandomSquare
	{
		public static Vector2 Area( ref UMT.MersenneTwister _rand )
		{
			// Move to -1, 1 space as for CIRCLE and SPHERE
			return new Vector2((2*_rand.NextSingle(true) - 1), (2*_rand.NextSingle(true) - 1));
		}
		
		public static Vector2 Area( ref UMT.MersenneTwister _rand, MTRandom.Normalization n, float t )
		{
			float x,y;
			x = y = 0;
			switch (n) {
			case MTRandom.Normalization.STDNORMAL:
				x = (float) NormalDistribution.Normalize(_rand.NextSingle(true), t);
				y = (float) NormalDistribution.Normalize(_rand.NextSingle(true), t);
			break;
			case MTRandom.Normalization.POWERLAW:
				x = (float) PowerLaw.Normalize(_rand.NextSingle(true), t, 0, 1);
				y = (float) PowerLaw.Normalize(_rand.NextSingle(true), t, 0, 1);
			break;
			default:
				x = _rand.NextSingle(true);
				y = _rand.NextSingle(true);
			break;
			}
			
			// Move to -1, 1 space as for CIRCLE and SPHERE
			return new Vector2((2*x - 1), (2*y - 1));
		}
	}
}