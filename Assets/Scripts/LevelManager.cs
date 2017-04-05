﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// This script does the following:
/// 	Ends the level when the music stops playing
/// 	Loads the level with a bitmap if given one
/// 	If not given one, it makes one in an xml file
/// </summary>
public class LevelManager : MonoBehaviour {

	public Conductor conductor;
	public GameObject toFollow1;
	public GameObject ObjWithAudio;

	//using the audio handler to see if the level has ended
	private new AudioSource audio;

	//This is an arraylist to write player input to. This will help me map out the beatmap
	ArrayList beatMap;

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
		audio = ObjWithAudio.GetComponent<AudioSource>();
		beatMap = new ArrayList();
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

			//go to menu (this script should never not be on a scenehandler object)
			this.gameObject.GetComponent<Scenehandler>().AdvanceScene();
		}
	}
}
