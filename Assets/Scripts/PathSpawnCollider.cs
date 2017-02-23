using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawnCollider : MonoBehaviour {

	/// <summary>
	/// This class is what handles the spawning of the next platform once the 
	/// trigger is hit. Note that for now I'm setting the path as a simple level
	/// but it could be changed later to select from a list in a pseudorandom way.
	/// </summary>
	//The Path game object is the object that holds the path prefab.
	public GameObject Path;
	public GameObject NextPlatPointer;

	//Adjustment amount is just 3x the lenght of the platform
	private Vector3 AdjustmentAmount = new Vector3(78, 0, 0);


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "WhitePlayer")
		{			
			//Now with object pooling!
			if(NextPlatPointer != null)
			{
				NextPlatPointer.transform.position = NextPlatPointer.transform.position + AdjustmentAmount;
			}
			////Where the next point should be spawned.
			//Vector3 vector = new Vector3(this.transform.position.x + 26,0,0);
			////spawn next platform.
			//Instantiate(Path, vector, this.transform.rotation);
		}
	}
}
