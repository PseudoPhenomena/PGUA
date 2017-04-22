using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

	public GameObject SceneManager;//Reference to the scene manager
	public GameObject Content;//The Content object on the scroll view where text goes
	public GameObject Menu;
	

	private string fileName;
	public Text textPrefab;

	// Use this for initialization
	void Start () {
		fileName = SceneManager.GetComponent<LevelManager>().ObjWithAudio.GetComponent<AudioSource>().clip.name + "Leaderboard";
		TextAsset leaderBoard = Resources.Load(fileName) as TextAsset;

		//Parse the text asset.
		string lb = leaderBoard.text;
		string[] LBLines = Regex.Split(lb, @"\n");

		
		//Parse again to get rank, name and score values.
		for(int i = 0; i < LBLines.Length; i++)
		{
		   
			string[] values = Regex.Split(LBLines[i],";");

			Debug.Log(values[0]);
			Debug.Log(values[1]);
			Debug.Log(values[2]);
			
			string text = values[0] + " " + values[1] + " " + values[2];

			Text newText = Instantiate(textPrefab, transform) as Text;
			newText.transform.SetParent(Content.transform, false);
			newText.text = text;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Switch()
	{
		Menu.SetActive(false);
	}
}
