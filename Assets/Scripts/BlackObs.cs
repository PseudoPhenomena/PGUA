using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackObs : MonoBehaviour {
    public float time;
    public void Instantiate(Vector3 spawn)
    {
        gameObject.transform.position = spawn;
    }
}
