using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColButton : MonoBehaviour {

	//The possible sprites for a button.

	public Sprite[] buttonSprites = new Sprite[5];
	//Keeps track of what sprite we're on
	private int pos;
	[HideInInspector]
	public BeatObject info;
	//The current sprite on the button
	[HideInInspector]
	public Image currImg;
	//This boolean helps to set whether the button is 'high' or not.
	public bool high;

	// Use this for initialization
	void Start () {
		pos = 1;
		currImg = GetComponent<Image>();
		info = new BeatObject();
		info.high = high;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// This is the method that will change the sprite of the button. And update
	/// the column's information.
	/// 
	/// TODO: Update the object for the column so that the button in this space
	/// has been changed
	/// </summary>
	public void ChangeButton()
	{
		if (pos <= 4)
		{
			currImg.sprite = buttonSprites[pos];
		}
		else
		{
			pos = 0;
			currImg.sprite = buttonSprites[pos];
		}
		pos++;
		info.state = currImg.sprite.name;
	}
}
