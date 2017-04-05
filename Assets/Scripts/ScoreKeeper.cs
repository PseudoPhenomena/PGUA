using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	// character particle manager references
	public CharacterParticleManager Top;
	public CharacterParticleManager Bot;

	// Score ratio that is accessible to any
	// Will eventually take into account both runner and conversation
	[HideInInspector]
	public float scoreRatio { get; private set; }
	[HideInInspector]
	public int score { get; private set; }
	private int tokensCollected = 0;
	public const int scoreIncDecAmount = 100;

	// if the player is doing well
	private bool onfire = false;

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
		tokensCollected++;
		OnFireCheck ();
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
		OnFireCheck ();
	}

	// If we let players jump
	public void Miss()
	{
		total++;

		// avoids divide by 0 errors (shouldn't happen, but gaurentees it)
		scoreRatio = (total > 0) ? hits / total : 0;
	}

	// checks to se if the player has collected 5 tokens and is above a 50% collection rate
	private void OnFireCheck()
	{
		// if the player is on fire, check to see if they should stop being on fire
		if (!onfire && Top != null && Bot != null) 
		{
			if (scoreRatio > .2 && tokensCollected > 4) 
			{
				Bot.StartAura ();
				Top.StartAura ();
				onfire = true;
			}
		} 
		// if the player is not on fire, check to see if they should start being on fire
		else if (Top != null && Bot != null)
		{
			if (scoreRatio < .2) 
			{
				Bot.StopAura ();
				Top.StopAura ();
				onfire = false;
			}
		}
	}
}
