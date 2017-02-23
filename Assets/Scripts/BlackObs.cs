using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackObs : MonoBehaviour {
    public float time;
    public void Instantiate(Vector3 spawn, float time)
    {
        gameObject.transform.position = spawn;
        this.time = time;
    }
}
