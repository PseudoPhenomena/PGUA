using System;
using System.Collections;
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
	/// <summary>
	/// Reference to the parent of all the different faces
	/// on the ui; "CuteFacePanel"
	/// </summary>
	public GameObject UIFaces;
	public GameObject toFollow1;
	public GameObject ObjWithAudio;
	public GameObject BeatList;

	// what song to play for each situation
	private Dictionary<string, string> levelMap = new Dictionary<string, string>()
	{
		// first level with mrbones is the tutorial
		{"Mr Bones 0", "Slime Girls - Bonfires(BPM109)" },
		{"Jean 0", "Ride on Shooting Star(137bpm)" },
		{"Emo 0", "Prom Night(124bpm)" },
		{"Mr Bones 1", "Chase(BPM225)" },
		{"Jean 1", "battlecry-nujabes(100bpm)" },
		{"Emo 1", "omniboi - Nice Dream - 06 Jollipop(128BPM)" },
		// please fill these out, i don't know the names of the songs or i would =(
	};
	//Reference to the leaderboard UI object.
	public GameObject Scoreboard;

	//using the audio handler to see if the level has ended
	private new AudioSource audio;

	//This is an arraylist to write player input to. This will help me map out the beatmap
	ArrayList beatMap;

	// score keeper ref
	public ScoreKeeper sk;

	//The filepath of the beatmap
	string fileName;

	//Stuff for writing to the beatmap
	private float lastBeat;
	private float crotchet;
	private float bpm;
	private int beat;
	private string playerSpeed;
	private bool end;//is the level over?

	// Use this for initialization
	void Start() {        
		// turn off scoreboard
		Scoreboard.SetActive(false);

		// put up the right face
		ChangeFacePanel();

		// this should get you the npc's name
		string key = SceneLoadSettings.CurrentSettings.npcName;
		// this should add their progress with that character to the key 
		//(if name doesn't have stored data, returns "0")
		key += " " + DataManager.data.GetProgress(key);
        Debug.Log(key + " key");
		// if this key has a song attached to it
		if (levelMap.ContainsKey(key))
		{
            Debug.Log("contains key");
			// this should fetch a gameobject with the right song on it
			string songName = levelMap[key];
			GameObject song = BeatList.transform.Find(songName).gameObject;

			audio = song.GetComponent<AudioSource>(); 
			beatMap = new ArrayList();

			if (audio != null)
			{
                // this should probably make use of songName to get the file path
				fileName = Application.dataPath + "/Resources/Music/" + audio.clip.name + "(" + playerSpeed + ")" + "beatmap.txt";
				bpm = conductor.bpm;
				Debug.Log("Dynamic Filepath: " + fileName);
			}
			else
			{
				DefaultLevelSetup();
			}
		}
		else
		{
			DefaultLevelSetup();
		}

        audio.Play();

		//Stuff for writing to the beatmap
		lastBeat = 0;
		crotchet = 60 / bpm;
		beat = 0;
		end = false;
	}

	private void DefaultLevelSetup()
	{
		//audio = song.GetComponent<AudioSource>(); 
		audio = ObjWithAudio.GetComponent<AudioSource>();
		beatMap = new ArrayList();

		// this should probably make use of songName to get the file path
		fileName = Application.dataPath + "/Resources/Music/" + audio.clip.name + "(" + playerSpeed + ")" + "beatmap.txt";
		bpm = conductor.bpm;
		Debug.Log("Static Filepath: " + fileName);
	}

	private void ChangeFacePanel()
	{
		string name = SceneLoadSettings.CurrentSettings.npcName;
		GameObject face = null;
		// try to find a face to match the name of the npc
		if (name.Length >= 1)
		{
			face = UIFaces.transform.Find(SceneLoadSettings.CurrentSettings.npcName).gameObject;
		}

		GameObject defaultFace = UIFaces.transform.Find("Default").gameObject;

		// if none is found
		if (face == null)
		{
			// go with our default image from before
			face = defaultFace;
		}

		//defaultFace.SetActive(false);
		face.SetActive(true);
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
			//This block writes the beatmap that is used in the level editor
			if (fileName != null && !File.Exists(fileName))
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

			int highscore = DataManager.data.TopScore;

			if (sk.score > highscore)
				DataManager.data.TopScore = sk.score;

			string character = SceneLoadSettings.CurrentSettings.npcName;
			if (character != null) 
			{
				if (character == "Emo") 
				{
					DataManager.data.Emo++;
				}
				else if (character == "MrBones") 
				{
					DataManager.data.MrBones++;
				}
				else if (character == "Dere") 
				{
					DataManager.data.Dere++;
				}
				else if (character == "Jean") 
				{
					DataManager.data.Jean++;
				}
			}

			if (!Scoreboard.activeInHierarchy && !end)
			{
				Scoreboard.SetActive(true);
				Scoreboard.GetComponent<LeaderBoard>().UpdateBoard(sk.score);
				end = true;

			}
			if (!Scoreboard.activeInHierarchy && end)
			{
				//go to menu (this script should never not be on a scenehandler object)
				Scenehandler sh = this.gameObject.GetComponent<Scenehandler>();
				sh.AdvanceScene(); 
			}
		}
	}

	
}
