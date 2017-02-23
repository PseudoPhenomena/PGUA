using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceButton : MonoBehaviour {

	public string option;
	public DialogueManager box;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetText(string newText)
	{
		//this.GetComponentInChildren<Text>().text = newText;
	}

	public void SetOption(string newOption)
	{
		this.option = newOption;
	}

	/// <summary>
	/// This as is allows the game to check if the scene is going to change or
	/// if it is just changing the line. If it is just changing the line it does
	/// that in the dialogue manager. If it's changing the scene it uses a Unity
	/// to load the scene desired.
	/// </summary>
	public void ParseOption()
	{
		string command = option.Split(',')[0];
		string commandModifier = option.Split(',')[1];
		box.playerTalking = false;
		if (command == "line")
		{
			box.lineNum = int.Parse(commandModifier);
			
		}
		else if (command == "scene")
		{
			//Application.LoadLevel("Scene" + commandModifier);This has been depricated, if I need it, do as below
			SceneManager.LoadScene("Scene" + commandModifier);
		}
	}
}
