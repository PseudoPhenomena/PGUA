using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPoolScript : MonoBehaviour {

	public GameObject WhiteTokenPool;
	public GameObject BlackTokenPool;

	[HideInInspector]
	public LinkedList<SpawnEvent.BeatObstacle> queue;
}
