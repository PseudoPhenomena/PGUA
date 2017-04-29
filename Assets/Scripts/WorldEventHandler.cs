using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldEventHandler : MonoBehaviour {
    public GameObject SceneManagerObeject;
    public GameObject AnimationHandler;

    private Scenehandler sHandler;
    private WorldCharAnimations location;
    private void Start()
    {
        sHandler = SceneManagerObeject.GetComponent<Scenehandler>();
        location = AnimationHandler.GetComponent<WorldCharAnimations>();
    }

    // Update is called once per frame
    void Update () {

		//This will eventually check where the player is
		if (Input.GetButtonDown("Submit"))
		{
			Debug.Log("Loading Scene");
            if (sHandler == null)
            {
                Debug.Log("No given scene handler, using default");
                SceneManager.LoadScene("Conversation");
            }
            else
            {
                LoadNext();
            }
		}
	}

    private void LoadNext()
    {
        if (location != null)
        {
            string loc = location.CurrentLocation;

            sHandler.AdvanceScene();
        }
        else
            Debug.Log("No refference to {WorldCharAnimations} given to {WorldEventHandler}.");
    }
}
