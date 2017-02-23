using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script defines the behavior of the camera as follows:
/// 
/// The camera moves along at the same speed of the player objects.
/// When a player character makes a mistake they fall back, and they lose points.
/// (Which is handled in a different script.)
/// If a player character falls off screen this triggers the lose condition.(Probably
/// handled in the player script too)
/// </summary>
public class CameraFollow : MonoBehaviour {

	//The two objects that we want to follow
	public GameObject toFollow1;
	public GameObject toFollow2;

	//Variable to adjust the x offset of the camera.
	public int xOffset;

	//Midpoint between them that the camera stays focused on
	private float midpoint;

	//Units to keep from players
	public float zoomFactor;
	public float followTimeDelta;

	//Camera offset variable
	private Vector3 offset;

	//using the audio handler to see if the level has ended
	private new AudioSource audio;

	//This is an arraylist to write player input to. This will help me map out the beatmap
	ArrayList beatMap;

	//The filepath of the beatmap
	string fileName;
	// Use this for initialization
	void Start() {
		Vector3 toAdd = new Vector3(xOffset, 0, 0);
		offset = transform.position + toAdd;
		audio = GetComponent<AudioSource>();
		midpoint = ((toFollow1.transform.position + toFollow2.transform.position) / 2).y;
		beatMap = new ArrayList();
		fileName = "C:\\Users\\Hayden\\Documents\\Waifu Runner 3D\\Assets\\" + audio.clip.name + "beatmap.txt";
	}
	//Update is called once per frame
	void Update()
	{
		//If space is pressed, swap the players

		if (Input.GetButtonDown("Switch"))
		{
			Debug.Log("Swap: " + audio.time);
			beatMap.Add("Swap: " + audio.time);
		}

		//JumpCode
		//Move it up when the jump button is pressed.
		if (Input.GetButtonDown("JumpTop"))
		{
			Debug.Log("JumpTop: " + audio.time);
			beatMap.Add("JumpTop: " + audio.time);
		}
		//Move it back when it's released
		if (Input.GetButtonUp("JumpTop"))
		{
			Debug.Log("JumpTop Release: " + audio.time);
			beatMap.Add("JumpTop Release: " + audio.time);
		}


		//JumpCode
		//Move it up when the jump button is pressed
		if (Input.GetButtonDown("JumpBottom"))
		{
			Debug.Log("JumpBot: " + audio.time);
			beatMap.Add("JumpBot: " + audio.time);
		}
		//Move it back when it's released
		if (Input.GetButtonUp("JumpBottom"))
		{
			Debug.Log("JumpBot Release: " + audio.time);
			beatMap.Add("JumpBot Release: " + audio.time);
		}
	}
	// LateUpdate is called once after each frame
	void LateUpdate () {
		Vector3 midline = new Vector3(toFollow1.transform.position.x, midpoint, toFollow1.transform.position.z);
		transform.position = midline + offset;

		if (!audio.isPlaying)
		{
			//Code to write button presses to file
			using (StreamWriter sw = new StreamWriter(fileName))
			{
				Debug.Log("Writing to file");
				foreach (string el in beatMap)
				{
					//print the string to a text file

					sw.WriteLine(el);

				}
			}
			//go to menu
			SceneManager.LoadScene(0);
		}
	}
}
