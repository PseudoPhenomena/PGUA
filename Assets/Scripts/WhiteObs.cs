using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteObs : MonoBehaviour {

	public float time;
	/// <summary>
	/// I've called it instantiate because what it does is it takes an instantiated
	/// object and moves it to the location determined in the other script.
	/// </summary>
	public void Instantiate(Vector3 spawn, float time)
	{
		gameObject.transform.position = spawn;
		this.time = time;
	}
}
