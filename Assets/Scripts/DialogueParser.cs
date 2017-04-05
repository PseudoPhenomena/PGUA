using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DialogueParser : MonoBehaviour {

	List<DialogueLine> lines;
	List<DialogueLine> interruptions;

	public string file;
	/// <summary>
	/// Here is a struct that defines the characteristics of a line of dialogue.
	/// I'd like to give credit up front to the Indiana Universitie's Game Developers
	/// Group for doing most of the legwork for this in their visual novel tutorial.
	/// </summary>
	struct DialogueLine
	{
		public string name;
		public string content;
		public int pose;
		public string position;
		public string[] options;

		public DialogueLine(string Name, string Content, int Pose, string Position)
		{
			name = Name;
			content = Content;
			pose = Pose;
			position = Position;
			options = new string[0];
		}
	}

	// Use this for initialization
	void Start () {

		///NOTE:
		///I'm hardcoding this for the prototype, but the idea is pretty straight
		///forward. And should give us the ability to load a variety of scripts.
		///Something along the format of CharacterName + DateNumber + .XML if we
		///choose too.
		///For now though the format for a .txt file will do.
		Scene current = SceneManager.GetActiveScene();
		string sceneNum = current.name;
		sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
		file += sceneNum;
		file += ".txt";//I will likely change this to XML later.

		lines = new List<DialogueLine>();
		LoadDialogue(file);
	}

	

	private void LoadDialogue(string filename)
	{
		string line;
		TextAsset script = Resources.Load(filename) as TextAsset;
		StreamReader r = new StreamReader(Application.dataPath + "/Resources/"+filename);

		using (r)
		{
			do
			{
				line = r.ReadLine();
				if (line != null)
				{
					string[] lineData = line.Split(';');
					if (lineData[0] == "Player")
					{
						DialogueLine lineEntry = new DialogueLine(lineData[0], "", 0, "");
						lineEntry.options = new string[lineData.Length - 1];

						for (int i = 1; i < lineData.Length; i++)
						{
							lineEntry.options[i - 1] = lineData[i];
						}
						lines.Add(lineEntry);
					}
					else
					{
						DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], int.Parse(lineData[2]), lineData[3]);
						lines.Add(lineEntry);
					}                    
				}
			}
			while (line != null);
			r.Close();
		}
	}

	public string GetPosition(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].position;
		}
		return "";
	}

	public string GetName(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].name;
		}
		return "";
	}

	public string GetContent(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].content;
		}
		return "";
	}

	public int GetPose(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].pose;
		}
		return 0;
	}

	public string[] GetOptions(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].options;
		}
		return new string[0];
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
