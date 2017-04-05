using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterMissedTokenScript : MonoBehaviour {

	//Object that keeps track of Score
	public GameObject ScoreKeeperObj;
	public GameObject TokenManagerObj;

	//Determined at runtime
	private ScoreKeeper ScoreKeeperScript;
	private TokenPoolScript TokenManager;

	////This is the dialogue manager we will do it with.
	//private DialogueManager dm;

	// Use this for initialization
	void Start () {
		//dm = panel.GetComponent<DialogueManager>();
		ScoreKeeperScript = ScoreKeeperObj.GetComponent<ScoreKeeper>();
		TokenManager = TokenManagerObj.GetComponent<TokenPoolScript> ();
	}

	void OnTriggerEnter(Collider col)
	{
		//decrease score
		ScoreKeeperScript.OtherColorHit();

		// cycle token
		TokenManager.TokenDestroy(col.gameObject);
	}
}
