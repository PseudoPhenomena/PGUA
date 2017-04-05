using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldEventHandler : MonoBehaviour {
    public Scenehandler sHandler;
    public WorldCharAnimations location;

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
            if (location.current == 0) // school
            {
                // how i intend it to work
                //SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("School Conversation", false, "Emo");

                // for now
                SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Emo");
            }
            else if (location.current == 1) // graveyard
            {
                // how i intend it to work
                //SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Graveyard Conversation", false, "Emo");

                // for now
                SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Emo");
            }
            else
            {
                //default
                SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Emo");
            }
        }
        else
            Debug.Log("No refference to {WorldCharAnimations} given to {WorldEventHandler}.");
    }
}
