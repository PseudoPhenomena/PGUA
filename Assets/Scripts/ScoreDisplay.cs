using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    // Object that this can query for score
    public GameObject ScoreKeeperObj;
    private ScoreKeeper ScoreKeeperScript;

    private Text scoreDisplay;

    private float score;
    private string preText = "Score: ";

	// Use this for initialization
	void Start () {
        ScoreKeeperScript = ScoreKeeperObj.GetComponent<ScoreKeeper>();
        scoreDisplay = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (score != ScoreKeeperScript.score)
        {
            score = ScoreKeeperScript.score;
            scoreDisplay.text = preText + score.ToString();
        }
	}
}
