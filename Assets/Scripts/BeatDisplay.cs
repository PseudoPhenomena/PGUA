using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatDisplay : MonoBehaviour {

	public GameObject conductor;
	public GameObject songSource;

	private Conductor conductorRef;
	private InputField beatDisplay;
	private AudioSource song;

	// Use this for initialization
	void Start () {
		conductorRef = conductor.GetComponent<Conductor>();
		beatDisplay = GetComponent<InputField>();
		song = songSource.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (song.isPlaying)
		{
			beatDisplay.text = conductorRef.beatNumber.ToString(); 
		}
		
	}

	/// <summary>
	/// Resets the display to zero
	/// </summary>
	public void reset()
	{
		conductorRef.beatNumber = 0;
	}
}
