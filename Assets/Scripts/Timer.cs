using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	private Text time;
	private AudioSource clip;
	public GameObject audioSource;
	// Use this for initialization
	void Start () {
		time = GetComponent<Text>();
		clip = audioSource.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		time.text = clip.time.ToString();
	}
}
