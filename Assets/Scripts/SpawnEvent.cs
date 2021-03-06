﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SpawnEvent : MonoBehaviour {

	//This is a struct that defines the objects as read from the XML file
	public struct BeatObstacle
	{
		public Vector3 spawnPoint;
		public string color;
		public string side;
		public string high;//This determines if it's high or low
	}

	//This is the object the music is playing from.
	public GameObject audioBeat;

	//	Token pool manager, needed to be able to hand it the spawn queues
	public GameObject TokenPoolManager;

	//Name of the file to be loaded. I think there is a better way to do this but this will do for now.
	public string filename;

	///These are kind of the markers for where we want to spawn, what they actually
	///are, are a spot just ahead of the players where the obstacles should spawn.
	public GameObject TopChar;
	public GameObject BotChar;

	/// Position vectors of the top and bottom character
	/// by adjusting the x, you get a point in their path
	private Vector3 topAdj;
	private Vector3 botAdj;

	//The objects we wish to create when spawnEnemy is called.
	public GameObject whiteObs;
	public GameObject blackObs;

	private float xSpot;
	//These are the y coordinates of the spawn point.
	
	//The TOP and BOTTOM spawn points for obstacles.
	private float TOP = 3.34f;
	private float BOT = -2.2f;

	//This is the list that it reads the game objects into that the beginning of a level.
	private List<BeatObstacle> beatMap;
	private List<BeatObstacle>.Enumerator en;

	// Spawn queues handed off to the map token pool script
	private LinkedList<BeatObstacle> whiteQueue = new LinkedList<BeatObstacle> ();
	private LinkedList<BeatObstacle> blackQueue = new LinkedList<BeatObstacle> ();

	//This is the XmlDoc to be read
	private XmlDocument xmlDoc;
	private string _fileName;
	private TextAsset textXml;

	// Use this for initialization
	void Start () {
		//audioBeat.GetComponent<BeatDetection>().CallBackFunction = MyCallbackEventHandler;
		xSpot = TopChar.transform.position.x + 10;

		topAdj = TopChar.transform.position;
		topAdj.x = 0;

		botAdj = BotChar.transform.position;
		botAdj.x = 0;

		loadXMLFromAssets();
		readXml();

		//	new way of spawning all tokens:
		//		give the queues to token pool manager
		//		call its populate method to set up the first round of tokens
		//		when tokens are consumed or missed, they are moved to a new location by the pool
		if (TokenPoolManager != null) 
		{
			// hand off the queues
			TokenPoolScript tpScript = TokenPoolManager.GetComponent<TokenPoolScript>();
			tpScript.blackQueue = blackQueue;
			tpScript.whiteQueue = whiteQueue;

			// populate the first round of tokens
			tpScript.PopulateLevel ();
		} 
		else 
		{
			// report missing TPM
			Debug.Log("Spawn event manager was not given a reference to token pool manager.");
		}
	}


	private void readXml()
	{
		float x;
		foreach(XmlElement node in xmlDoc.SelectNodes("//obstacle"))
		{
			BeatObstacle tempObstacle = new BeatObstacle();
			tempObstacle.color = node.SelectSingleNode("color").InnerText;
			tempObstacle.side = node.SelectSingleNode("side").InnerText;
			tempObstacle.high = node.SelectSingleNode("high").InnerText;

			x = float.Parse(node.SelectSingleNode("x").InnerText);

			if (tempObstacle.side.Equals("top") && tempObstacle.high.Equals("False"))
			{
				tempObstacle.spawnPoint = topAdj + new Vector3(x, 0, 0);
			}
			else if (tempObstacle.side.Equals("bot") && tempObstacle.high.Equals("False"))
			{
				tempObstacle.spawnPoint = botAdj + new Vector3(x, 0, 0);
			}
			else if(tempObstacle.side.Equals("top") && tempObstacle.high.Equals("True"))
			{
				tempObstacle.spawnPoint = topAdj + new Vector3(x, 2.0f, 0);
			}
			else if(tempObstacle.side.Equals("bot") && tempObstacle.high.Equals("True"))
			{
				tempObstacle.spawnPoint = botAdj + new Vector3(x, 2.0f, 0);
			}

			//A method call to a test method that displays the info in tempObstacle
			//displayData(tempObstacle);

			if (tempObstacle.color == "White" || tempObstacle.color == "white") {
				whiteQueue.AddLast (tempObstacle);
			} else if (tempObstacle.color == "Black" || tempObstacle.color == "black") {
				blackQueue.AddLast (tempObstacle);
			} else {
				Debug.Log ("Color did not match known: " + tempObstacle.color);	
			}
		}
	}

	private void displayData(BeatObstacle tempObstacle)
	{
		Debug.Log(tempObstacle.color + "\n" + tempObstacle.side + "\n" + tempObstacle.spawnPoint + "\n");
	}

	/// <summary>
	/// Simple method that loads the XML file stored in assets
	/// </summary>
	private void loadXMLFromAssets()
	{
		xmlDoc = new XmlDocument();
		Debug.Log("*****************Here is where the error was: " + getPath() + "*****************");
		if (System.IO.File.Exists(getPath()))
		{
			xmlDoc.LoadXml(System.IO.File.ReadAllText(getPath()));
		}
		else
		{
			textXml = (TextAsset)Resources.Load(_fileName, typeof(TextAsset));
			xmlDoc.LoadXml(textXml.text);
		}
	}

	private string getPath()
	{
		#if UNITY_EDITOR
			return Application.dataPath + "/Resources/Music/" + _fileName + ".xml";
		#elif UNITY_ANDROID
			return Application.persistentDataPath+filename;
		#elif UNITY_IPHONE
			return GetiPhoneDocumentsPath()+"/"+filename;
		#else
			return Application.dataPath + "/Resources/Music/" + filename + ".xml";
		#endif
	}

	// Update is called once per frame
	void Update () {
		xSpot = TopChar.transform.position.x + 10;
	}

	void Awake()
	{
		//TODO: Make loading the xml maps dynamic
		_fileName = filename;
		beatMap = new List<BeatObstacle>();

	}
	/// <summary>
	/// TODO: This does nothing for now. But I'm leaving it in at the moment as an example
	/// </summary>
	/// <param name="eventinfo"></param>
	public void MyCallbackEventHandler(BeatDetection.EventInfo eventinfo)
	{
		//switch (eventinfo.messageInfo)
		//{
		//	case BeatDetection.EventType.Energy:
		//		//Debug.Log("Energy Detected!");
		//		SpawnEnemy();
		//		break;
		//	case BeatDetection.EventType.HitHat:
		//		//Debug.Log("HiHat Detected!");
		//		SpawnEnemy();
		//		break;
		//	case BeatDetection.EventType.Kick:
		//		//Debug.Log("Kick detected");
		//		SpawnEnemy();
		//		break;
		//	case BeatDetection.EventType.Snare:
		//		//SnareCode here
		//		//Debug.Log("Snare detected!");
		//		//StartCoroutine(showText(snare, gsnare));
		//		SpawnEnemy();
		//		break;
		//}
	}

	/// <summary>
	/// This is the method that will be called from the method above to spawn the
	/// enemies on the path.
	/// 
	/// For the time being, not that energy spawns the black obstacles. While 
	/// the drums spawn the white ones. This is likely to change, since I see it
	/// causing problems later on.
	/// </summary>
	private void SpawnEnemy()
	{
		if (en.MoveNext())
		{

			Vector3 spawn;

			BeatObstacle next = en.Current;

			spawn = next.spawnPoint;
			//Debug.Log(spawn);
			
			if (next.color.Equals("Black"))
			{
				GameObject obj = Instantiate(blackObs);
				obj.GetComponent<BlackObs>().Instantiate(spawn);
			}
			else if (next.color.Equals("White"))
			{
				GameObject obj = Instantiate(whiteObs);
				obj.GetComponent<WhiteObs>().Instantiate(spawn);
			}
			
		}
	}
}
