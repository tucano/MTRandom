using UnityEngine;
using System;
using UMT;

/// <summary>
/// MT random main class.
/// In Unity3d there is already a Random number generator based on the platform-specific random generator. 
/// Here we present an alternative Random library for Unity3d designed to generate uniform Pseudo-Random deviates. 
/// The library use a fast PRNG (Mersenne-Twister) to generate: Floating Number in range [0-1] and in range [n-m], Vector2 and Vector3 data types. 
/// Which kind of transformations I can apply to the random uniform deviates?
/// The uniform deviates can be transformed with the distributions: Standard Normal Distribution and Power-Law. 
/// In addition is possible to generate floating random deviates coming from other distributions: Poisson, Exponential and Gamma.
/// </summary>
public class MTRandom
{
	private MersenneTwister _rand;

	#region SEED
	/// <summary>
	/// Initializes a new instance of the <see cref="MTRandom"/> class.
	/// </summary>
	public MTRandom()
	{
		_rand = new MersenneTwister();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="MTRandom"/> class.
	/// </summary>
	/// <param name="seed">Seed (integer).</param>
	public MTRandom(int seed)
	{
		_rand = new MersenneTwister(seed);
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="MTRandom"/> class.
	/// </summary>
	/// <param name="phrase">Phrase (seed string).</param>
	public MTRandom(string phrase)
	{
		char[] values = phrase.ToCharArray();
		System.Int32[] keys = new System.Int32[values.Length];
		for (int i = 0; i < values.Length; i++) 
		{
			// Get the integral value of the character. 
			keys[i] = System.Convert.ToInt32(values[i]);
		}
		_rand = new MersenneTwister(keys);
	}
	#endregion

	#region VALUE
	/// <summary>
	/// Returns a pseudo-random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only).
	/// </summary>
	/// <returns>
	/// This method returns a single-precision pseudo-random number greater than or equal to zero, and less
	/// than or equal to one.
	/// </returns>
	public float value()
	{
		return _rand.NextSingle(true);
	}
	/// <summary>
	/// Returns a pseudo-random number greater than or equal to zero, and either strictly
	/// less than one, or less than or equal to one, depending on the value of the
	/// given boolean parameter.
	/// </summary>
	/// <param name="includeOne">
	/// If <see langword="true"/>, the pseudo-random number returned will be 
	/// less than or equal to one; otherwise, the pseudo-random number returned will
	/// be strictly less than one.
	/// </param>
	/// <returns>
	/// If <paramref name="includeOne"/> is <see langword="true"/>, this method returns a
	/// single-precision pseudo-random number greater than or equal to zero, and less
	/// than or equal to one. If <paramref name="includeOne"/> is <see langword="false"/>, 
	/// this method returns a single-precision pseudo-random number greater than or equal to zero and
	/// strictly less than one.
	/// </returns>
	public float value(bool includeOne)
	{
		return _rand.NextSingle(includeOne);
	}
	/// <summary>
	/// Returns a normalized pseudo-random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only). 
	/// </summary>
	/// <returns>
	/// This method returns a single-precision pseudo-random number greater than or equal to zero, and less
	/// than or equal to one.
	/// </returns>
	/// <param name="temperature">Temperature.</param>
	public float valueNorm(float temperature)
	{
		return (float) NormalDistribution.Normalize(_rand.NextSingle(true), temperature);
	}
	/// <summary>
	/// Returns a power-law pseudo-random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only). 
	/// </summary>
	/// <returns>
	/// This method returns a single-precision pseudo-random number greater than or equal to zero, and less
	/// than or equal to one.
	/// </returns>
	/// <param name="temperature">Temperature.</param>
	public float valuePower(float temperature)
	{
		return (float) PowerLaw.Normalize(_rand.NextSingle(true),temperature,0,1);
	}
	/// <summary>
	/// Returns a pseudo-random number in Poisson distribution between 0.0 [inclusive] and 1.0 [inclusive] (Read Only). 
	/// </summary>
	/// <returns>The value.</returns>
	public float valuePoisson(float lambda)
	{
		return PoissonDistribution.Normalize( ref _rand, lambda);
	}
	/// <summary>
	/// Returns a pseudo-random number in Exponential distribution between 0.0 [inclusive] and 1.0 [inclusive] (Read Only). 
	/// </summary>
	/// <returns>The value.</returns>
	public float valueExponential(float lambda)
	{
		return ExponentialDistribution.Normalize( _rand.NextSingle( false ), lambda );
	}
	/// <summary>
	/// Returns a pseudo-random number on the gamma distribution.
	/// </summary>
	/// <returns>The gamma value.</returns>
	/// <param name="order">Order.</param>
	public float valueGamma(int order)
	{
		return GammaDistribution.Normalize(ref _rand, order);
	}
	#endregion

	#region RANGE
	/// <summary>
	/// Returns the next pseudo-random number integer between <paramref name="min"/> [inclusive] and <paramref name="max"/> [inclusive].
	/// </summary>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Maximum.</param>
	public int Range(int min, int max)
	{
		return _rand.Next(min,max+1);
	}
	/// <summary>
	/// Returns the next pseudo-random number integer between <paramref name="min"/> [inclusive] and <paramref name="max"/> [depend on <paramref name="includeMax"/>].
	/// </summary>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Max.</param>
	/// <param name="includeMax">If set to <c>true</c> include <paramref name="Max"/>.</param>
	public int Range(int min, int max, bool includeMax)
	{
		if (includeMax)
		{
			return _rand.Next(min,max+1);
		}
		else
		{
			return _rand.Next(min,max);
		}
	}
	/// <summary>
	/// Returns the next pseudo-random number integer between <paramref name="min"/> [inclusive] and <paramref name="max"/> [inclusive].
	/// </summary>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Maximum.</param>
	public float Range(float min, float max)
	{
		return ScaleFloatToRange((float)_rand.NextSingle(true),min,max,0.0f,1.0f);
	}
	/// <summary>
	/// Returns the next pseudo-random number integer between <paramref name="min"/> [inclusive] and <paramref name="max"/> [inclusive].
	/// </summary>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Maximum.</param>
	/// <param name="temperature">Temperature.</param>
	public float RangeNorm(float min, float max, float temperature)
	{
		return ScaleFloatToRange((float) NormalDistribution.Normalize(_rand.NextSingle(true),temperature),min,max,0.0f,1.0f);
	}
	/// <summary>
	/// Returns the next pseudo-random number integer between <paramref name="min"/> [inclusive] and <paramref name="max"/> [inclusive].
	/// in Power Law distribution
	/// </summary>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Maximum.</param>
	/// <param name="temperature">Temperature.</param>
	public float RangePower(float min, float max, float temperature)
	{
		return ScaleFloatToRange((float) PowerLaw.Normalize(_rand.NextSingle(true),temperature,0,1),min,max,0.0f,1.0f);
	}
	#endregion

	#region COLOR
	/// <summary>
	/// Return the next pseudo-random number as a Color
	/// </summary>
	public Color color()
	{
		return WaveToRgb.LinearToRgb(_rand.NextSingle(true));
	}
	/// <summary>
	/// Return the next pseudo-random number as a Color with a linear scale [0.0-1.0]. 
	/// Use a range between <paramref name="min"/> [inclusive] and <paramref name="max"/> [inclusive] to reduce the color range.
	/// </summary>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Max.</param>
	public Color color(float min, float max)
	{
		if ((max <= min) || (min < 0.0f) || (max > 1.0f))
		{
			throw new ArgumentOutOfRangeException();
		}
		return WaveToRgb.LinearToRgb(ScaleFloatToRange((float)_rand.NextSingle(true),min,max,0.0f,1.0f));
	}
	#endregion

	#region VECTOR2
	public enum Normalization
	{
		STDNORMAL = 0,
		POWERLAW = 1
	}
	/// <summary>
	/// pseudo-random number as a point in a square.
	/// </summary>
	/// <returns>The in point as Vector2.</returns>
	public Vector2 PointInASquare()
	{
		return RandomSquare.Area(ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point in a square.
	/// </summary>
	/// <returns>The in point as Vector2.</returns>
	/// <param name="n">Normalization type (STDNORMAL or POWERLAW).</param>
	/// <param name="t">Temperature.</param>
	public Vector2 PointInASquare(Normalization n , float t )
	{
		return RandomSquare.Area(ref _rand, n, t);
	}
	/// <summary>
	/// pseudo-random number as a point in a circle.
	/// </summary>
	/// <returns>The in point as Vector2.</returns>
	public Vector2 PointInACircle()
	{			
		return RandomDisk.Circle(ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point in a circle.
	/// </summary>
	/// <returns>The in point as Vector2.</returns>
	/// <param name="n">Normalization type (STDNORMAL or POWERLAW).</param>
	/// <param name="t">Temperature.</param>
	public Vector2 PointInACircle(Normalization n, float t)
	{			
		return RandomDisk.Circle(ref _rand, n, t);
	}
	/// <summary>
	/// pseudo-random number as a point in a disk.
	/// </summary>
	/// <returns>The in point as Vector2.</returns>
	public Vector2 PointInADisk()
	{
		return RandomDisk.Disk(ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point in a disk.
	/// </summary>
	/// <returns>The in point as Vector2.</returns>
	/// <param name="n">Normalization type (STDNORMAL or POWERLAW).</param>
	/// <param name="t">Temperature.</param>
	public Vector2 PointInADisk(Normalization n, float t)
	{
		return RandomDisk.Disk(ref _rand, n, t);
	}
	#endregion

	#region VECTOR3
	/// <summary>
	/// pseudo-random number as a point in a cube.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	public Vector3 PointInACube()
	{
		return RandomCube.Volume(ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point in a cube.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	/// <param name="n">Normalization type (STDNORMAL or POWERLAW).</param>
	/// <param name="t">Temperature.</param>
	public Vector3 PointInACube(Normalization n, float t)
	{
		return RandomCube.Volume(ref _rand, n, t);
	}
	/// <summary>
	/// pseudo-random number as a point on a cube surface.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	public Vector3 PointOnACube()
	{
		return RandomCube.Surface(ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point on a cube surface.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	/// <param name="n">Normalization type (STDNORMAL or POWERLAW).</param>
	/// <param name="t">Temperature.</param>
	public Vector3 PointOnACube(Normalization n, float t)
	{
		return RandomCube.Surface(ref _rand, n, t);
	}
	/// <summary>
	/// pseudo-random number as a point on a sphere surface.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	public Vector3 PointOnASphere()
	{
		return RandomSphere.Surface(ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point in a sphere.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	public Vector3 PointInASphere()
	{
		return RandomSphere.Volume(ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point on a cap surface.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	public Vector3 PointOnCap(float spotAngle)
	{
		return RandomSphere.GetPointOnCap(spotAngle, ref _rand);
	}
	/// <summary>
	/// pseudo-random number as a point on a ring surface.
	/// </summary>
	/// <returns>The in point as Vector3.</returns>
	public Vector3 PointOnRing(float innerAngle, float outerAngle)
	{
		return RandomSphere.GetPointOnRing(innerAngle, outerAngle, ref _rand);
	}
	#endregion

	#region FUNCTIONS
	/// <summary>
	/// Scales the float to any range.
	/// </summary>
	/// <returns>The float to range.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="newMin">New minimum.</param>
	/// <param name="newMax">New max.</param>
	/// <param name="oldMin">Old minimum.</param>
	/// <param name="oldMax">Old max.</param>
	public static float ScaleFloatToRange(float x, float newMin, float newMax, float oldMin, float oldMax)
	{
		return (x / ((oldMax - oldMin) / (newMax - newMin))) + newMin;
	}
	#endregion
}
