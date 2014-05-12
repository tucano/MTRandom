using UnityEngine;
using System.Collections;
using UMT;

public class MersenneTwisterTest : MonoBehaviour 
{
	/// <summary>
	/// The random generator.
	/// </summary>
	private MersenneTwister _rand_one;
	private MersenneTwister _rand_two;
	private MersenneTwister _rand_three;
	private MersenneTwister _rand_four;

	// Use this for initialization
	void Start () 
	{
		Debug.Log("STARTED with RANDOM SEED");
		_rand_one = new MersenneTwister();

		string list = "";

		for (int i = 0; i < 1000; i++) {
			list += _rand_one.Next(-1,1).ToString() + ", ";
		}
		Debug.Log("RANDOM NEXT: " + list);

		list = "";
		for (int i = 0; i < 1000; i++) {
			list += _rand_one.NextUInt32(2,3).ToString() + ", ";
		}
		Debug.Log("RANDOM NEXT RANGE: " + list);


		Debug.Log("STARTED with RANDOM SEED: 23");
		_rand_two = new MersenneTwister(23);
		Debug.Log("RANDOM NEXT: " + _rand_two.NextUInt32());


		Debug.Log("STARTED with RANDOM SEED: WORD");
		string input = "Winter is coming";
		char[] values = input.ToCharArray();
		System.Int32[] keys = new System.Int32[values.Length];
		for (int i = 0; i < values.Length; i++) 
		{
			// Get the integral value of the character. 
			keys[i] = System.Convert.ToInt32(values[i]);
		}
		_rand_three = new MersenneTwister(keys);
		Debug.Log("RANDOM NEXT: " + _rand_three.NextUInt32());


		Debug.Log("STARTED with RANDOM SEED: 23");
		_rand_four = new MersenneTwister(23);
		Debug.Log("RANDOM NEXT: " + _rand_four.NextUInt32());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
