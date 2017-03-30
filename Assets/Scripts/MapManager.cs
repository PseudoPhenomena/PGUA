using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

	//This is the content object in the scroll view that the columns are drawn on
	public RectTransform Content;
	//There isn't a file explorer built into Unity. I may buy one later. But until then I'm just gonna get the files this way.
	//Input text boxes
	public InputField OpenNewBox;
	public InputField OpenExistingBox;
	public InputField ExportBox;

	//This dictionary is the map of each beat. Using a dictionary just cuz.
	[HideInInspector]
	public Dictionary<int, Column> BeatMap;

	
	//Where we store the column prefab for spawning
	public GameObject col;

	//XmlDoc object should help with loading
	private XmlDocument xmlDoc;
	private TextAsset textXml;
	private string fileName;
	// Use this for initialization
	void Start () {
		BeatMap = new Dictionary<int, Column>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// This method updates the beatmap
	/// </summary>
	public void UpdateMap(int index, Column newCol)
	{
		BeatMap[index] = newCol;
		Debug.Log("Beat Number:" + BeatMap[index].BeatNumber);
		Debug.Log("XPos: " + BeatMap[index].pos);
	}

	/// <summary>
	/// This method operates on the export button click and exports the beatmap to an
	/// XML file
	/// </summary>
	public void ExportBeatMap()
	{

		Debug.Log("Exporting...");
		//First, get the text from the box, this is the name the file will get when saved.
		string fileName = ExportBox.text;
		//I don't know if the .xml is needed
		string filePath = Application.dataPath + "/Resources/Music/" + ExportBox.text + ".xml";

		//The process for writing to XML is rather simple.

		//The settings to help with formatting
		XmlWriterSettings settings = new XmlWriterSettings();
		settings.Indent = true;
		settings.IndentChars = "\t";

		//Start with setting up the XMLWriter we'll be using to write it in
		using (XmlWriter writer = XmlWriter.Create(filePath, settings))
		{
			writer.WriteStartDocument();
			writer.WriteStartElement("Beatmap");
			//write the number of beats as an attribute
			writer.WriteAttributeString("beats", BeatMap.Count.ToString());
			
			//Then we're gonna iterate over the entire Beatmap
			foreach(KeyValuePair<int, Column> entry in BeatMap)
			{
				
				//This float will hold the half way point between each beat and the next
				float halfway = ((BeatMap[entry.Key + 1].pos - BeatMap[entry.Key].pos) / 2) + BeatMap[entry.Key].pos;

				
				/*It's important to note what the information in each
				obstacle is going to be. Each obstacle should be exported with 
				the following information:
					1. The color of the obstacle
					2. The x-position of the obstacle(Where the beat is)
					3. Whether or not the obstacle is high or low
					4. What side the obstacle is on
					SPECIAL CASE:
					If it's a double it should give two obstacles. One on the beat
					and the other half way between the beat and the next one.*/
				//Since each column is made up of four buttons we go over 
				//each button to put it in the map.
				for(int i = 0; i < 4; i++)
				{
					ColButton currBtn = entry.Value.colButtons[i].GetComponent<ColButton>();
					//Ignore blanks
					if (!currBtn.currImg.sprite.Equals("Blank"))
					{
						string side = "error";
						string high = "error";
						string color = "error";
						string xPos = "error";
						string oppColor = "error";

						bool addBWDouble = false;
						bool addWBDouble = false;

						//If it's 0 or 1 it's on top
						if (i == 0 || i == 1)
						{
							side = "top";
						}
						//2 or 3 and it's on bot
						if (i == 2 || i == 3)
						{
							side = "bot";
						}

						//Simply check the .high property on the beat object and write that
						high = currBtn.high.ToString();

						//If the color is a double do this:
						addBWDouble = (currBtn.currImg.sprite.name.Equals("BlackWhiteDouble"));
						addWBDouble = (currBtn.currImg.sprite.name.Equals("WhiteBlackDouble"));
						if (addBWDouble) { color = "black"; oppColor = "white"; }
						else if (addWBDouble) { color = "white"; oppColor = "black"; }

						//If the color isn't a double do this:
						//Set the color to whatever the sprite shows
						else
						{
							color = currBtn.currImg.sprite.name;
						}

						//finally, just get the x-position from the button
						xPos = entry.Value.pos.ToString();

						if (!color.Equals("Blank"))
						{
							writer.WriteStartElement("obstacle");
							//Write the beat number as an attribute
							writer.WriteAttributeString("beat", entry.Key.ToString());
							//Also write down the image name. To check if it's a double later
							writer.WriteAttributeString("double", currBtn.currImg.sprite.name);
							writer.WriteElementString("side", side);
							writer.WriteElementString("high", high);
							writer.WriteElementString("color", color);
							writer.WriteElementString("x", xPos);
							writer.WriteEndElement(); 
						}

						//If there was a double, add one at the halfway point
						if (addBWDouble || addWBDouble)
						{
							//Start element
							writer.WriteStartElement("obstacle");
							//doubles are always on the same side
							writer.WriteElementString("side", side);
							//always the same height
							writer.WriteElementString("high", high);
							//the opposite color
							writer.WriteElementString("color", oppColor);
							//and at the 'halfway' point
							writer.WriteElementString("x", halfway.ToString());
							//End element
							writer.WriteEndElement();
						}
					} 
				}
			}
			//(Should) End the beatmap element
			writer.WriteEndElement();
			writer.WriteEndDocument();
		}
	}

	/// <summary>
	/// This method operates on the open new button, it reads the txt file and generates
	/// one column for each line.
	/// </summary>
	public void LoadNewMap()
	{
		Vector2 spawnPoint = new Vector3(-326f, 279f, 0f);
		string filePath = Application.dataPath + "/Resources/Music/" + OpenNewBox.text + ".txt";
		string line;

		StreamReader sr = new StreamReader(filePath);
		using (sr)
		{
			do
			{
				line = sr.ReadLine();
				if (line != null)
				{
					string[] lineData = line.Split(';');
					//Spawn columns on Content.
					GameObject newItem = Instantiate(col, spawnPoint, Quaternion.identity) as GameObject;
					newItem.transform.SetParent(Content.transform, false);
					/*It's important to note here that the txt file is split each line on the ;.
					The one on the left of the ; is the position, the one on the right is the 
					beat number*/
					//here we get the position and the beat number and make a column object
					Column newCol = newItem.GetComponent<Column>();
					newCol.pos = float.Parse(lineData[0]);
					newCol.BeatNumber = int.Parse(lineData[1]);
					Debug.Log(newCol.BeatNumber);
					BeatMap.Add(newCol.BeatNumber, newCol);

				}
			} while (line != null);

		}
	}

	/// <summary>
	/// So this method is a method that opens an existing map from a previously made
	/// XML file.
	/// </summary>
	public void LoadExistingMap()
	{

		fileName = Application.dataPath + "/Resources/Music/" + OpenExistingBox.text + ".xml";

		//First things first, clear the drawing board.
		foreach (Transform child in Content)
		{
			Destroy(child);
		}
		//Then look at the loaded XML files beats attribute. That's how many columns need to be drawn.
		loadXMLFromAssets();
		//Reading XML starts here.

		//Next read the objects in and write them to the proper column by beat.
	}

	private void loadXMLFromAssets()
	{
		xmlDoc = new XmlDocument();
		if (System.IO.File.Exists(getPath()))
		{
			xmlDoc.LoadXml(System.IO.File.ReadAllText(getPath()));
		}
		else
		{
			textXml = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
			xmlDoc.LoadXml(textXml.text);
		}
	}

	private string getPath()
	{
		#if UNITY_EDITOR
				return Application.dataPath + "/Resources/" + fileName;
		#elif UNITY_ANDROID
					return Application.persistentDataPath+fileName;
		#elif UNITY_IPHONE
					return GetiPhoneDocumentsPath()+"/"+fileName;
		#else
					return Application.dataPath + "/Resources/" + fileName;
		#endif
	}
}
