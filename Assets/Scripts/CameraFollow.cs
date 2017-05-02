using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// This script defines the behavior of the camera as 
/// always following the midpoint of the two player objects///
/// </summary>
public class CameraFollow : MonoBehaviour {

	//The two objects that we want to follow
	public GameObject toFollow1;
	public GameObject toFollow2;

	//Variable to adjust the x offset of the camera.
	public int xOffset;

	//Midpoint between them that the camera stays focused on
	private Vector3 midpoint;

	//Units to keep from players
	public float zoomFactor;
	public float followTimeDelta;

	//Camera offset variable
	private Vector3 offset;

	//Reference to the character object
	public GameObject character;

	// Use this for initialization
	void Start() {
		Vector3 toAdd = new Vector3(xOffset, 0, 0);
		offset = transform.position + toAdd;

        midpoint = ((toFollow1.transform.position + toFollow2.transform.position) / 2);
    }
    //Update is called once per frame
    void Update()
	{
        midpoint = ((toFollow1.transform.position + toFollow2.transform.position) / 2);

        transform.position = midpoint + offset;

	}
}
