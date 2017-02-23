using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DialogueManager : MonoBehaviour {

	DialogueParser parser;

	private string dialogue, characterName, playerName, playerCharacterText;
	public int lineNum;
	int pose;
	string position;
	string[] options;
	///In the original tutorial this was used to tell if the player was making a 
	///decision, here it could be used to pause the action on the screen for the
	///same reason. Or some other things.
	public bool playerTalking;
	List<Button> buttons = new List<Button>();

	public Text dialogueBox;
	public Text playerCharacterDialogue;
	public Text characterNameBox;
	public Text playerNameBox;
	public GameObject facePanel;
	public GameObject choiceBox;
	//This number represents which interruption the player is on.
	private int intNum;

	// Use this for initialization
	void Start () {
		dialogue = "";
		characterName = "";
		pose = 0;
		position = "L";
		playerTalking = false;
		parser = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
		lineNum = 0;
		intNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		///This will present a problem, we don't want to update it when there is
		///input from the mouse, but constantly. I'll come back to this.
		//if(Input.GetMouseButtonDown(0) && playerTalking == false)
		//{
		//	ShowDialogue();
		//	lineNum++;
		//}

		UpdateUI();
	}

	

	public void ShowDialogue()
	{
		//This is not implemented as intended yet.
		//TODO: Implement reset images correctly.
		//ResetImages();
		ParseLine();
	}

	/// <summary>
	/// This method does as it says and should reset the sprite that is currently
	/// displaying in the UI. However, since we don't have them currently defined
	/// as characters we have the option of implementing this in a different way.
	/// The only reason it's implemented here is to keep the code from erroring out.
	/// </summary>
	void ResetImages()
	{
		if(characterName != "")
		{
			GameObject character = GameObject.Find(characterName);
			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();

			currSprite.sprite = null;
		}
	}

	/// <summary>
	/// It is the job of this method to parse the next line in the script.
	/// It starts by getting the name of the character in the line we are on.
	/// If the name is "Player" we want to show dialogue options,(and possibly
	/// show no blocks on the path). 
	/// To show a normal nine, set playerTalking to false, then set all the variables to match
	/// Then call DisplayImages() to show the character image on the screen.(NOTE:
	/// This is from the IUPUI implementation, the character icon should always be
	/// shown.)
	/// This will call CreateButtons on 'Player' lines.
	/// </summary>
	public void ParseLine()
	{
		///At the moment the player name is Jack. This can be changed at any time.
		if(parser.GetName (lineNum) != "Jack")
		{
			playerTalking = false;
			characterName = parser.GetName(lineNum);
			dialogue = parser.GetContent(lineNum);
			pose = parser.GetPose(lineNum);
			position = parser.GetPosition(lineNum);
		}
		else
		{
			playerTalking = false;
			playerName = parser.GetName(lineNum);
			playerCharacterText = parser.GetContent(lineNum);
			pose = parser.GetPose(lineNum);
			position = parser.GetPosition(lineNum);
			//CreateButtons();
		}
		lineNum++;
	}

	/// <summary>
	/// This method will work a lot like the one above. However, I don't have similar
	/// behavior defined for the interruptions script. The ideal implementation is to 
	/// print the interruption. And then the normal line. Since I don't have this 
	/// functionality yet.
	/// 
	/// As such, the solution for now is to just take the interruption and append the
	/// next normal dialogue line onto it and to send that to the dialogue box.
	/// 
	/// However, make sure it's just using Jacks current line and interruption.
	/// TODO: Make sure it's only using Jacks lines!
	/// </summary>
	public void ParseInterruption()
	{
		if (parser.GetName(lineNum) == "Jack")
		{
			playerTalking = false;
			playerName = parser.GetName(lineNum);
			playerCharacterText = parser.getInterruption(intNum) + parser.GetContent(lineNum);
			Debug.Log("Jack say's" + playerCharacterText);
			pose = parser.GetPose(lineNum);
			position = parser.GetPosition(lineNum);
			intNum++;
			lineNum++; 
		}
		else
		{
			playerTalking = false;
			playerName = parser.GetName(lineNum);
			playerCharacterText = parser.getInterruption(intNum);
			dialogue = parser.GetContent(lineNum);
			Debug.Log("Jack say's" + playerCharacterText);
			pose = parser.GetPose(lineNum);
			position = parser.GetPosition(lineNum);
			intNum++;
			lineNum++;
		}
	}
	/// <summary>
	/// This method looks rather straight forward. It's purpose is to generate
	/// buttons for each player choice outlined in the script. This seems useful
	/// So I'm going to see if I can't find a way to use it somehow.
	/// 
	/// TODO: Fix Implementation
	/// </summary>
	void CreateButtons()
	{
		for (int i = 0; i < options.Length; i++)
		{
			GameObject button = (GameObject)Instantiate(choiceBox);
			Button b = button.GetComponent<Button>();
			ChoiceButton cb = button.GetComponent<ChoiceButton>();
			cb.SetText(options[i].Split(':')[0]);
			cb.option = options[i].Split(':')[1];
			cb.box = this;
			b.transform.SetParent(this.transform);
			b.transform.localPosition = new Vector3(0, -25 + (i * 50));
			b.transform.localScale = new Vector3(1, 1, 1);
			buttons.Add(b);
		}
	}

	/// <summary>
	/// This method is probably superfluous but it's supposed to allow the game to
	/// decide where it wants to place the sprite on the screen. I'll either remove
	/// this, or change it so that it displays it a certain way on the panel
	/// </summary>
	/// <param name="character"></param>
	void SetSpritePositions(GameObject spriteObj)
	{
		if (position == "L")
		{
			spriteObj.transform.position = new Vector3(-6, 0);
		}
		else if (position == "R")
		{
			spriteObj.transform.position = new Vector3(6, 0);
		}
		spriteObj.transform.position = new Vector3(spriteObj.transform.position.x, spriteObj.transform.position.y, 0);
	}

	/// <summary>
	/// After the dialogue is parsed we have to update the UI using this function
	/// If the player isn't 'talking' then we should clear all buttons. Then set
	/// the dialogue text and name to the appropriate string.
	/// </summary>
	void UpdateUI()
	{
		if (!playerTalking)
		{
			ClearButtons();
		}
		dialogueBox.text = dialogue;
		characterNameBox.text = characterName;
		playerCharacterDialogue.text = playerCharacterText;
		playerNameBox.text = playerName;

	}

	/// <summary>
	/// Simply loops through the buttons created for decisions and destroys them.
	/// </summary>
	void ClearButtons()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			print("Clearing buttons");
			Button b = buttons[i];
			buttons.Remove(b);
			Destroy(b.gameObject);
		}
	}
}
