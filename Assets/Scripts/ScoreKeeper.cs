using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	// Score ratio that is accessible to any
	// Will eventually take into account both runner and conversation
	[HideInInspector]
	public float scoreRatio { get; private set; }
	[HideInInspector]
	public int score { get; private set; }
	public const int scoreIncDecAmount = 100;

	// Runner score variables (may end up being how score is passed)
	private float hits;
	private float total;

	// Use this for initialization
	void Start () {
		scoreRatio = 0f;		
	}
	
	public void SameColorHit()
	{
		hits++;
		total++;

		// avoids divide by 0 errors (shouldn't happen, but gaurentees it)
		scoreRatio = (total > 0) ? hits / total : 0;
		score += scoreIncDecAmount;
	}
	public void OtherColorHit()
	{
		total++;
		if (hits > 0)
			hits--;

		// avoids divide by 0 errors (shouldn't happen, but gaurentees it)
		scoreRatio = (total > 0) ? hits / total : 0;
		// Prevents negative score
		score -= (score > scoreIncDecAmount) ? scoreIncDecAmount : score;
	}

	// If we let players jump
	public void Miss()
	{
		total++;

		// avoids divide by 0 errors (shouldn't happen, but gaurentees it)
		scoreRatio = (total > 0) ? hits / total : 0;
	}
}
