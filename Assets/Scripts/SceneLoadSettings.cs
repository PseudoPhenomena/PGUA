using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadSettings : MonoBehaviour {
	public static Settings LoadSettings;
	public static Settings CurrentSettings;

	// settings passed between scenes
	public class Settings
	{
		/// <summary>
		/// Settings for the next scene to use when it loads
		/// </summary>
		/// <param name="_location">Which scene to load</param>
		/// <param name="_locationIsLevel">Is the next scene a runner level</param>
		/// <param name="_bpm">LEVEL ONLY: Needed BPM for the next level</param>
		/// <param name="_npcName">LEVEL ONLY: Name of the character the player is conversing with</param>
		/// <param name="_hardmode">LEVEL ONLY: Whether the level should be extra difficult</param>
		public Settings(string _location, bool _locationIsLevel, string _npcName = "", int _bpm = -1, bool _hardmode = false)
		{
			location = _location;
			locationIsLevel = _locationIsLevel;
			bpm = _bpm;
			npcName = _npcName;
			hardmode = _hardmode;
		}

		public string location;
		public bool locationIsLevel;
		public int bpm;
		public string npcName;
		public bool hardmode;
	}
}
