using UnityEngine;
using System.Collections;
using UMT;

public class MTRandomTester : MonoBehaviour 
{
	private MTRandom mrand;
	public string user_seed = "seed string here";
	public int max_objects = 1000;
	public float temperature = 1.0f;
	public float sphereRadius = 1.0f;
	private bool rotate = true;
	// This should be set to degrees per second
	public float rotationAmount = 20.0f;

	/* LateUpdate is called after all Update functions have been called. */
	void LateUpdate ()
	{
		if (Input.GetButtonDown("Jump")) {
			rotate = !rotate;
		}
	}

	void Update()
	{
		if (rotate) transform.Rotate (0, rotationAmount * Time.deltaTime, 0);
	}

	// Use this for initialization
	void Start () 
	{
		TestSeed();
	}

	void OnGUI()
	{
		GUILayout.BeginVertical("box");
		GUILayout.Label("MT RANDOM EXAMPLES:");

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("SEED:");
		user_seed = GUILayout.TextField(user_seed, 30);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.Label("Temperature = " + temperature.ToString());
		temperature = GUILayout.HorizontalSlider(temperature, 0.0f, 10.0f);

		if (GUILayout.Button("mrand.value()")) RandomValues();
		if (GUILayout.Button("mrand.valueNorm(temperature)")) RandomNormValues();
		if (GUILayout.Button("mrand.valuePower(temperature)")) RandomPowerValues();

		if (GUILayout.Button("IN SPHERE VOLUME")) ExampleInSphere();
		if (GUILayout.Button("IN SPHERE SURFACE")) ExampleOnSphere();
		if (GUILayout.Button("IN DISK")) ExampleInDisk();
		if (GUILayout.Button("ON RING")) ExampleOnRing();
		if (GUILayout.Button("ON CAP")) ExampleOnCap();
		if (GUILayout.Button("IN CUBE")) ExampleInCube();
		if (GUILayout.Button("ON CUBE")) ExampleOnCube();

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("TOGGLE ROTATION WITH JUMP");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();
	}

	private void ExampleInCube()
	{
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		DebugStreamer.AddMessage("mrand.PointInACube()");
		mrand = new MTRandom(user_seed);
		CleanUp();		
		MakeObjects();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject item in objs) 
		{
			item.transform.position = ScalePosition(mrand.PointInACube());
		}
	}

	private void ExampleOnCube()
	{
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		DebugStreamer.AddMessage("mrand.PointOnACube()");
		mrand = new MTRandom(user_seed);
		CleanUp();		
		MakeObjects();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject item in objs) 
		{
			item.transform.position = ScalePosition(mrand.PointOnACube());
		}
	}

	private void ExampleOnCap()
	{
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		DebugStreamer.AddMessage("mrand.PointOnCap(30.0f)");
		mrand = new MTRandom(user_seed);
		CleanUp();
		MakeObjects();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject item in objs) 
		{
			item.transform.position = ScalePosition(mrand.PointOnCap(30.0f));
		}
	}

	private void ExampleOnRing()
	{
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		DebugStreamer.AddMessage("mrand.PointOnRing(20.0f, 30.0f)");
		mrand = new MTRandom(user_seed);
		CleanUp();
		MakeObjects();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject item in objs) 
		{
			item.transform.position = ScalePosition(mrand.PointOnRing(20.0f, 30.0f));
		}
	}

	private void ExampleInDisk()
	{
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		DebugStreamer.AddMessage("mrand.PointInADisk()");
		mrand = new MTRandom(user_seed);
		CleanUp();		
		MakeObjects();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject item in objs) 
		{
			item.transform.position = ScalePosition(mrand.PointInADisk());
		}
	}

	private void ExampleOnSphere()
	{
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		DebugStreamer.AddMessage("mrand.PointOnASphere()");
		mrand = new MTRandom(user_seed);
		CleanUp();		
		MakeObjects();
	}

	private void ExampleInSphere()
	{
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		DebugStreamer.AddMessage("mrand.PointInASphere()");
		mrand = new MTRandom(user_seed);
		CleanUp();		
		MakeObjects();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject item in objs) 
		{
			item.transform.position = ScalePosition(mrand.PointInASphere());
		}
	}

	private void MakeObjects()
	{
		// CENTER OF THE SPHERE: x = 0, y = 20, z = 0 I generate an object there
		GameObject _pivot = new GameObject();
		_pivot.transform.position = new Vector3(0,0,0);

		for (int i = 0; i < max_objects; i++) 
		{
			// WE make a small sphere
			GameObject _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			// WE SCALE THE SPHERE
			_sphere.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
			// TAKE A SCALED RANDOM POSITION
			Vector3 pos = ScalePosition(mrand.PointOnASphere());			
			// WE set the sphere position
			_sphere.transform.position = pos;
			// WE give a tag to the sphere
			_sphere.tag = "Player";
			// remove collider
			Destroy(_sphere.transform.collider);
			_sphere.renderer.material.color = mrand.color();
			_sphere.transform.parent = transform;
		}
	}

	private Vector3 ScalePosition(Vector3 pos)
	{
		pos = Vector3.Scale(pos, new Vector3(sphereRadius,sphereRadius,sphereRadius));
		return pos;
	}

	private void RandomPowerValues()
	{
		CleanUp();
		DebugStreamer.AddMessage("Returns a power law pseudo-random number between 0.0 [inclusive] and 1.0 [inclusive]");
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		mrand = new MTRandom(user_seed);
		DebugStreamer.AddMessage("mrand.valuePower(" + temperature.ToString() + ")");
		for (int i = 0; i < max_objects; i++)
		{
			float x = mrand.valuePower(temperature);
			float x_position = MTRandom.ScaleFloatToRange(x, -2.0f, 2.0f, 0.0f, 1.0f);
			GameObject sphere = MakeObject(new Vector3(x_position,x_position,0.0f));
			sphere.name = x.ToString();
		}
	}

	private void RandomNormValues()
	{
		CleanUp();
		DebugStreamer.AddMessage("Returns a normalized pseudo-random number between 0.0 [inclusive] and 1.0 [inclusive]");
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		mrand = new MTRandom(user_seed);
		DebugStreamer.AddMessage("mrand.valueNorm(" + temperature.ToString() + ")");
		for (int i = 0; i < max_objects; i++)
		{
			float x = mrand.valueNorm(temperature);
			float x_position = MTRandom.ScaleFloatToRange(x, -2.0f, 2.0f, 0.0f, 1.0f);
			GameObject sphere = MakeObject(new Vector3(x_position,x_position,0.0f));
			sphere.name = x.ToString();
		}
	}

	private void RandomValues()
	{
		CleanUp();
		DebugStreamer.AddMessage("Returns a pseudo-random number between 0.0 [inclusive] and 1.0 [inclusive]");
		DebugStreamer.AddMessage("MTRandom mrand = new MTRandom(seed)");
		mrand = new MTRandom(user_seed);
		DebugStreamer.AddMessage("mrand.value()");
		for (int i = 0; i < max_objects; i++)
		{
			float x = mrand.value();
			float x_position = MTRandom.ScaleFloatToRange(x, -2.0f, 2.0f, 0.0f, 1.0f);
			GameObject sphere = MakeObject(new Vector3(x_position,x_position,0.0f));
			sphere.name = x.ToString();
		}
	}

	private void CleanUp()
	{
		// Clear all
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject item in objs) 
		{
			Destroy(item);	
		}
	}

	private GameObject MakeObject(Vector3 pos)
	{
		GameObject _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		_sphere.transform.parent = transform;
		_sphere.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		_sphere.tag = "Player";
		_sphere.transform.position = pos;
		// remove collider
		Destroy(_sphere.transform.collider);
		return _sphere;
	}

	private void TestSeed()
	{
		int seed = 12345678;
		string phrase = "seed test";
		MTRandom mrand_int = new MTRandom(seed);
		MTRandom mrand_string = new MTRandom(phrase);
		DebugStreamer.AddMessage("Runnig seed test:");
		string test_result;
		float value = mrand_int.value();
		test_result = value.ToString("0.0000000") == "0.2458042" ? "OK" : "NOT OK";
		DebugStreamer.AddMessage("Using seed NUMBER: " + seed + " EXPECTING: 0.2458042 RESULT: " + test_result);
		value = mrand_string.value();
		test_result = value.ToString("0.0000000") == "0.1944317" ? "OK" : "NOT OK";
		DebugStreamer.AddMessage("Using seed STRING: \"" + phrase + "\" EXPECTING: 0.1944317 RESULT: " + test_result);
	}
}
