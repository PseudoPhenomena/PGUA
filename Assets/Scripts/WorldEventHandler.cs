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
            // remember where the player is
            string loc = location.CurrentLocation;
            SceneLoadSettings.lastLocation = loc;

            SetNextScene();

            sHandler.AdvanceScene();
        }
        else
            Debug.Log("No refference to {WorldCharAnimations} given to {WorldEventHandler}.");
    }

    // progression
    private void SetNextScene()
    {
        // they haven't played the tutorial
        if(DataManager.data.MrBones == 0)
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Mr Bones");
        }
        // this should just cycle them through some more levels
        else if (DataManager.data.Dere == 0)
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Dere");
        }
        else if (DataManager.data.Jean == 0)
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Jean");
        }
        else if (DataManager.data.MrBones == 1)
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Mr Bones");
        }
        else if (DataManager.data.Dere == 1)
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Dere");
        }
        else if (DataManager.data.Jean == 1)
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Jean");
        }
        // default level
        else
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Conversation", false, "Emo");
        }
    }
}
