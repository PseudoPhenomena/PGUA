using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPoolScript : MonoBehaviour {

	public GameObject WhiteTokenPool;
	public GameObject BlackTokenPool;
	public GameObject ActiveTokenPool;

	[HideInInspector]
	public LinkedList<SpawnEvent.BeatObstacle> whiteQueue;
	[HideInInspector]
	public LinkedList<SpawnEvent.BeatObstacle> blackQueue;

	public void TokenDestroy(GameObject token)
	{
		WhiteObs w = token.GetComponent<WhiteObs> ();
		BlackObs b = token.GetComponent<BlackObs> ();

		// token is white
		if (w != null && whiteQueue != null) 
		{
			if (whiteQueue.Count != 0) 
			{
				SpawnEvent.BeatObstacle nextLocation = whiteQueue.First.Value;
				whiteQueue.RemoveFirst ();

				token.transform.position = nextLocation.spawnPoint;
			} 
			else 
			{
				token.transform.parent = WhiteTokenPool.transform;
				token.SetActive (false);
			}
		} 
		// token is black
		else if (b != null && blackQueue != null) 
		{
			if (blackQueue.Count != 0) 
			{
				SpawnEvent.BeatObstacle nextLocation = blackQueue.First.Value;
				blackQueue.RemoveFirst ();
				token.transform.position = nextLocation.spawnPoint;
			} 
			else 
			{
				token.transform.parent = BlackTokenPool.transform;
				token.SetActive (false);
			}
		}
	}

	public void PopulateLevel()
	{
		// queue should be handed off before this is called
		// if it is null there is a serious problem
		if (whiteQueue != null && blackQueue != null) 
		{
			SpawnEvent.BeatObstacle nextLocation;
			GameObject token;

			// spawn <=20 white tokens
			while (whiteQueue.Count > 0 && // still objects to spawn
			      WhiteTokenPool.transform.childCount > 0) { // still tokens to place

				// pop the next location off the queue
				nextLocation = whiteQueue.First.Value;
				whiteQueue.RemoveFirst ();

				// pop the next token off the pool
				token = WhiteTokenPool.transform.GetChild(0).gameObject;
				token.transform.parent = ActiveTokenPool.transform;

				// activate the token
				token.transform.position = nextLocation.spawnPoint;
				token.SetActive (true);
			}

			// spawn <=20 black tokens
			while (blackQueue.Count > 0 && // still objects to spawn
				BlackTokenPool.transform.childCount > 0) { // still tokens to place

				// pop the next location off the queue
				nextLocation = blackQueue.First.Value;
				blackQueue.RemoveFirst ();

				// pop the next token off the pool
				token = BlackTokenPool.transform.GetChild(0).gameObject;
				token.transform.parent = ActiveTokenPool.transform;

				// activate the token
				token.transform.position = nextLocation.spawnPoint;
				token.SetActive (true);
			}
		} 
		else 
		{
			// report the absence of
			Debug.Log ("No queue found when trying to populate the level.");	
		}
			
	}
}
