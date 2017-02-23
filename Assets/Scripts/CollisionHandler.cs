using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

	//This should be checked if the attached character can affect dark pickups
	public bool isBlack;
	//This object is the panel we want to send the procs for character updates too.
	//public GameObject panel;

	//Object that keeps track of Score
	public GameObject ScoreKeeperObj;
	private ScoreKeeper ScoreKeeperScript;//Deturmined at runtime

	//This is the dialogue manager we will do it with.
	private DialogueManager dm;

	// Use this for initialization
	void Start () {
		//dm = panel.GetComponent<DialogueManager>();
		ScoreKeeperScript = ScoreKeeperObj.GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		//If the object is black and the pickup is white
		if (isBlack && col.gameObject.tag == "WhiteEnemy")
		{
			//Output interruption to chat log.
			//dm.ParseInterruption();
			//This constitutes an interruption, so we need to change the character panels image

			//decrease score
			ScoreKeeperScript.OtherColorHit();

			//Destroy pickup
			Destroy(col.gameObject);
		}
		//if the object is black and the pickup is black
		else if (isBlack && col.gameObject.tag == "BlackEnemy")
		{

			//Increase score
			ScoreKeeperScript.SameColorHit();
			//Destroy pickup
			Destroy(col.gameObject);
		}
		//If the object is white and the pickup is black
		else if (!isBlack && col.gameObject.tag == "BlackEnemy")
		{

			//Change character pose.

			//Decrease Score
			ScoreKeeperScript.OtherColorHit();

			//Destroy pickup
			Destroy(col.gameObject);
		}
		//If the object is white and the pickup is white
		else if (!isBlack && col.gameObject.tag == "WhiteEnemy")
		{
			//Increase score
			ScoreKeeperScript.SameColorHit();
			//Destroy pickup
			Destroy(col.gameObject);
		}
	}
}
