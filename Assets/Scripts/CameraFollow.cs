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

	public Conductor conductor;

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

    //Reference to the character object
    public GameObject character;

	//The filepath of the beatmap
	string fileName;

	//Stuff for writing to the beatmap
	private float lastBeat;
	private float crotchet;
	private float bpm;
	private int beat;
    private string playerSpeed;

    // Use this for initialization
    void Start() {
		Vector3 toAdd = new Vector3(xOffset, 0, 0);
		offset = transform.position + toAdd;
		audio = GetComponent<AudioSource>();
		midpoint = ((toFollow1.transform.position + toFollow2.transform.position) / 2).y;
		beatMap = new ArrayList();
        playerSpeed = character.GetComponent<CharacterMovement>().speed.ToString();
		fileName = Application.dataPath + "/Resources/Music/" + audio.clip.name + "(" + playerSpeed + ")" + "beatmap.txt";
		bpm = conductor.bpm;
		Debug.Log("Filepath: " + fileName);

		//Stuff for writing to the beatmap
		lastBeat = 0;
		crotchet = 60 / bpm;
		beat = 0;
	}
	//Update is called once per frame
	void Update()
	{
		if(conductor.songPosition > lastBeat + crotchet)
		{
			//Debug.Log("Beat");

			beat++;
			lastBeat += crotchet;

			beatMap.Add(toFollow1.transform.position.x + ";" + beat);
		}
	}
	// LateUpdate is called once after each frame
	void LateUpdate () {
		Vector3 midline = new Vector3(toFollow1.transform.position.x, midpoint, toFollow1.transform.position.z);
		transform.position = midline + offset;

		if (!audio.isPlaying)
		{
			//Code to to file
			if (!File.Exists(fileName))
			{
				using (StreamWriter sw = new StreamWriter(fileName))
				{
					Debug.Log("Writing to file");
					foreach (string el in beatMap)
					{
						//print the string to a text file

						sw.WriteLine(el);

					}
				} 
			}
            //go to menu
            SceneManager.LoadScene(0);
        }
	}
}
