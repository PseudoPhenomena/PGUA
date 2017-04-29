using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationLevelSettingsManager : MonoBehaviour {
    public GameObject SchoolBackground;
    public GameObject GraveyardBackground;
        
    public void SetUpConversation()
    {
        string npcName = SceneLoadSettings.CurrentSettings.npcName;
        string loc = SceneLoadSettings.CurrentSettings.location;

        if (SchoolBackground != null && GraveyardBackground != null)
        {
            if (npcName == "Mr Bones")
            {
                GraveyardBackground.SetActive(true);
                SchoolBackground.SetActive(false);
            }
            else
            {
                GraveyardBackground.SetActive(false);
                SchoolBackground.SetActive(true);
            }
        }
    }
}
