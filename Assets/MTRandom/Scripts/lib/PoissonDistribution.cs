using System;

//
// NEW VERSION FROM NUMERICAL RECIPES http://www.nrbook.com/a/bookcpdf.php
// 7.3 Rejection Method: Gamma, Poisson, Binomial Deviates

namespace UMT
{
	/// <summary>
	/// Poisson distribution.
	/// </summary>
	public static class PoissonDistribution
	{	
		// Builtin arrays (native .NET arrays), are extremely fast and efficient but they can not be resized.			
		static double[] cof = new double[6] {
			76.18009172947146,
			-86.50532032941677,
			24.01409824083091,
			-1.231739572450155,
			0.1208650973866179e-2,
			-0.5395239384953e-5};

		/// <summary>
		/// Gammln the specified xx.
		/// 6.1 Gamma Function, Beta Function, Factorials, Binomial Coefficients 
		/// http://www.nrbook.com/a/bookcpdf/c6-1.pdf
		/// Return the natural log of a gamma function for xx > 0
		/// Internal arithmetic in double precision.
		/// </summary>
		/// <param name="xx">Xx.</param>
		public static double gammln( double xx )
		{
			double x,y,tmp,ser;
			
			int j;
			
			y = x = xx;
			tmp = x + 5.5;
			tmp -= (x + 0.5) * Math.Log(tmp);
			ser=1.000000000190015;
			
			for (j=0;j<=5;j++) 
			{
				ser += cof[j]/++y;	
			}
			
			return -tmp+Math.Log(2.5066282746310005 * ser/x );
		}

		/// <summary>
		/// return as a floating point number an integer value that is a random deviate drawn 
		/// from a Possion Distribution of mean xm using randx as a source of uniform deviates
		/// </summary>
		/// <param name="_rand">random generator.</param>
		/// <param name="xm">Xm.</param>
		public static float Normalize( ref UMT.MersenneTwister _rand, float xm)
		{
			// Davide Rambaldi: all moved to double precision			
			double sq, alxm, g, oldm; // oldm is a flag for wheter xm has changed or not sincle last call
			sq = alxm = g = oldm = (-1.0);
			double em, t, y;			
			
			if (xm < 12.0f) {      // Use direct method				
				if (xm != oldm) {
					oldm = xm;
					g = Math.Exp(-xm); // if x is new, compute the exponential
				}
				em = -1;
				t = 1.0f;				
				// Instead of adding exponential deviates it is equivalent to multiply unifomr deviates
				// We never actually take the log, we compare the precomupted exponential				
				do {
					++em;
					t *= _rand.NextSingle(true);
				} while (t > g);				
			} else {
				// Use REJECTION method
				// xm has changed?
				if ( xm != oldm) {
					oldm = xm;
					sq = Math.Sqrt(2.0 * xm);
					alxm = Math.Log(xm);
					
					// Gammln is the natural log of a gamma function
					g = xm * alxm - gammln(xm + 1.0f);
				}
				do {	
					do {
						// y is the deviate from a Lorentzian comparison function
						y = Math.Tan(Math.PI*_rand.NextSingle(true));
						em=sq*y+xm;						
					} while (em < 0.0);
					em = Math.Floor(em);
					t = 0.9 * (1.0+y*y) * Math.Exp(em*alxm-gammln(em+1.0f)-g);
				} while (_rand.NextSingle(true) > t);
			}			
			return (float) em;
		}
		
	}
	
}