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
	public GameObject TokenManagerObj;

	//Determined at runtime
	private ScoreKeeper ScoreKeeperScript;
	private TokenPoolScript TokenManager;
	private CharacterParticleManager ParticleManager;

	////This is the dialogue manager we will do it with.
	//private DialogueManager dm;

	// Use this for initialization
	void Start () {
		//dm = panel.GetComponent<DialogueManager>();
		ScoreKeeperScript = ScoreKeeperObj.GetComponent<ScoreKeeper>();
		TokenManager = TokenManagerObj.GetComponent<TokenPoolScript> ();
		ParticleManager = GetComponent<CharacterParticleManager> ();
	}

	void OnTriggerEnter(Collider col)
	{
		
		//If the object is black and the pickup is white
		//MISMATCH
		if (isBlack && col.gameObject.tag == "WhiteEnemy")
		{
			//This is a mismatch, so we need to change the character panels image

			//decrease score
			ScoreKeeperScript.OtherColorHit();
            // play miss effect
            ParticleManager.PlayMiss();
            // cycle token
            TokenManager.TokenDestroy(col.gameObject);
		}
		//if the object is black and the pickup is black
		//MATCH
		else if (isBlack && col.gameObject.tag == "BlackEnemy")
		{
			//Increase score
			ScoreKeeperScript.SameColorHit();
			//Destroy pickup
			TokenManager.TokenDestroy(col.gameObject);
			//Play pick up particle
			ParticleManager.PlayPickup();
		}
		//If the object is white and the pickup is black
		//MISMATCH
		else if (!isBlack && col.gameObject.tag == "BlackEnemy")
		{
			//Change character pose.

			//Decrease Score
			ScoreKeeperScript.OtherColorHit();
            // play miss effect
            ParticleManager.PlayMiss();
            //Destroy pickup
            TokenManager.TokenDestroy(col.gameObject);
		}
		//If the object is white and the pickup is white
		//MATCH
		else if (!isBlack && col.gameObject.tag == "WhiteEnemy")
		{
			//Increase score
			ScoreKeeperScript.SameColorHit();
			//Destroy pickup
			TokenManager.TokenDestroy(col.gameObject);
			//Play pickup particle
			ParticleManager.PlayPickup();
		}
	}
}
