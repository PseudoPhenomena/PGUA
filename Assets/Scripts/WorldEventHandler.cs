using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldEventHandler : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		//This will eventually check where the player is
		if (Input.GetButtonDown("Submit"))
		{
			Debug.Log("Loading Scene");
			SceneManager.LoadScene(2);
		}
	}
}
