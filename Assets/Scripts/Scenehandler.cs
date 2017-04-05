using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenehandler : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Start");
        // if this isn't first time startup
        if (SceneLoadSettings.LoadSettings != null)
        {
            SceneLoadSettings.CurrentSettings = SceneLoadSettings.LoadSettings;
        }
        else // first time set up
        {
            //SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("World Map", false);
            SceneLoadSettings.CurrentSettings = new SceneLoadSettings.Settings("Main Menu", false);
        }

        // currently in a level
        if (SceneLoadSettings.CurrentSettings.locationIsLevel)
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("World Map", false);
        }
        // currently on world map
        else if (SceneLoadSettings.CurrentSettings.location == "World Map")
        {
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("Main Menu", false);
        }
        // currently on main menu /default
        else
        {
            Debug.Log("default");
            SceneLoadSettings.LoadSettings = new SceneLoadSettings.Settings("World Map", false);
        }
    }

    public void AdvanceScene()
    {
        Debug.Log(SceneLoadSettings.LoadSettings.location);
        SceneManager.LoadScene(SceneLoadSettings.LoadSettings.location);
    }
}
