﻿using UnityEngine;
using System.Collections;
using System.IO;

public class EventExample : MonoBehaviour {

    public GameObject AudioBeat;
    public GUIText energy;
    public GUIText kick;
    public GUIText snare;
    public GUIText hithat;

    public GameObject genergy;
    public GameObject gkick;
    public GameObject gsnare;
    public GameObject ghithat;

    public Material matOn;
    public Material matOff;

    public ArrayList beatMap;
    private AudioSource song;

    public void MyCallbackEventHandler(BeatDetection.EventInfo eventInfo)
    {
        switch(eventInfo.messageInfo)
        {
            case BeatDetection.EventType.Energy:
                StartCoroutine(showText(energy, genergy));
                beatMap.Add("Energy, " + song.time);
                break;
            case BeatDetection.EventType.HitHat:
                StartCoroutine(showText(hithat, ghithat));
                beatMap.Add("Hi-hat, " + song.time);
                break;
            case BeatDetection.EventType.Kick:
                StartCoroutine(showText(kick, gkick));
                beatMap.Add("Kick, " + song.time);
                break;
            case BeatDetection.EventType.Snare:
                StartCoroutine(showText(snare, gsnare));
                beatMap.Add("Snare, " + song.time);
                break;
        }
    }

    private IEnumerator showText(GUIText texto, GameObject objeto)
    {
        texto.enabled = true;
        objeto.GetComponent<Renderer>().material = matOn;
        yield return new WaitForSeconds(0.05f * 3);
        texto.enabled = false;
        objeto.GetComponent<Renderer>().material = matOff;
        yield break;
    }

    // Use this for initialization
    void Start () {
        //Register the beat callback function
        AudioBeat.GetComponent<BeatDetection>().CallBackFunction = MyCallbackEventHandler;
        song = AudioBeat.GetComponent<AudioSource>();
        beatMap = new ArrayList();
    }

}
