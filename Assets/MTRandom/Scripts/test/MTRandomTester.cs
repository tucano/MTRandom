using UnityEngine;
using System.Collections;
using UMT;

public class MTRandomTester : MonoBehaviour 
{
	private MTRandom mrand;
	private MTRandom mrand_int;
	private MTRandom mrand_string;
	private int seed = 12345678;
	private string phrase = "seed test";

	// Use this for initialization
	void Start () 
	{
		DebugStreamer.AddMessage("Runnig seed test:");
		string test_result;

		mrand_int = new MTRandom(seed);
		float value = mrand_int.value();
		test_result = value.ToString("0.0000000") == "0.2458042" ? "OK" : "NOT OK";
		DebugStreamer.AddMessage("Using seed NUMBER: " + seed + " EXPECTING: 0.2458042 RESULT: " + test_result);

		mrand_string = new MTRandom(phrase);
		value = mrand_string.value();
		test_result = value.ToString("0.0000000") == "0.1944317" ? "OK" : "NOT OK";
		DebugStreamer.AddMessage("Using seed STRING: \"" + phrase + "\" EXPECTING: 0.1944317 RESULT: " + test_result);

//		test_result = "";
//		float y = 0.0f;
//		for (int i = 0; i < 1000; i++) 
//		{
//			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//			sphere.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
//			sphere.transform.position = new Vector3(mrand_string.normRange(-1.0f,1.0f), y, 0.0f);
//			y += 0.1f;
//			sphere.renderer.material.color = mrand_string.color(0.3f,0.4f);
			//test_result += mrand_string.Range(-1.0f,1.0f) + ", ";
//		}
//		Debug.Log(test_result);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
