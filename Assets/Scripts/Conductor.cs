using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour {
	/// <summary>
	/// Note that the essential variables here are bpm, crotchet, offset, and songposition
	/// </summary>
	//Conductor
	//int crotchedsPerBar = 8;
	public float bpm = 127;//BPM of the song is going to change per song. For example, 127 is the bpm for You Belong.
	public float crotchet;//The time duration of a beat, calculated from the bpm with 60/bpm
	public float songPosition;
	public float deltaSongPos;
	//public float lastHit;// = 0.0f; the last snapped to beat.
	public float actualLastHit;
	//float nextBeatTime = 0.0f;
	//float nextBarTime = 0.0f;
	//An MP3 file has a tiny gap at the beginning to take into account, this is the offset variable
	public float offset = 0.2f; //positive should mean song must be minussed,
	public float addOffset;//Additional per level offset
	public static float offsetStatic = .4f;
	public static bool hasOffsetAdjusted = false;
	public int beatNumber = 0;
	public int barNumber = 0;
	
	public GameObject audioSource;
	//This is the starting point of the song.
	private double startSong;
	private AudioSource song;
	private float lastbeat;

	// Use this for initialization
	void Start () {
		song = audioSource.GetComponent<AudioSource>();
		crotchet = 60 / bpm;
		startSong = AudioSettings.dspTime;
		songPosition =(float)AudioSettings.dspTime;
		lastbeat = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//so the song.pitch - offset bit is about slowing down the playing speed apparently. I don't think it'll be necessary but I've commented here just in case.
		if (song.isPlaying)
		{
			songPosition = (float)(AudioSettings.dspTime - startSong); // * song.pitch - offset; 
		}
																   //Debug.Log("Song Position: " + songPosition);
																   //Debug.Log(lastbeat + crotchet);
		if (songPosition > lastbeat + crotchet && song.isPlaying)
		{
			//do action
			lastbeat += crotchet;
			beatNumber++;
		}
	}

	public void PauseSong()
	{
		song.Pause();
	}

	public void PlaySong()
	{
		if (song.isPlaying)
		{
			beatNumber = 0;
		}

		startSong = AudioSettings.dspTime;
		lastbeat = 0;
		song.Play();
	}

	public void StopSong()
	{
		song.Stop();
	}
}
