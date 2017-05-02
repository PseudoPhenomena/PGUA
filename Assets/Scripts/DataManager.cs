using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
	// unique data object for each play session
	// if we want to save, just write this to a file
	public static PersistantData data = new PersistantData();

	// struct for all persistant data
	public class PersistantData
	{
        /// <summary>
        /// way of looking up progress with a string of a characters name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetProgress(string name)
        {
            if (name == "Jean")
            { return Jean.ToString(); }
            else if (name == "Emo")
            { return Emo.ToString(); }
            else if (name == "MrBones")
            { return MrBones.ToString(); }
            else if (name == "Dere")
            { return Dere.ToString(); }

            return "0";
        }
		// the amount of levels
		// a player has beaten for each person
		public int Jean    = 0;
		public int Emo     = 0;
		public int MrBones = 0;
		public int Dere    = 0;

		// other generic data, I don't know what we want to have persist
		public int TopScore = 0;
		public string PlayerName;
	}
}
