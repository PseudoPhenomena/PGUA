using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

	public GameObject SceneManager;//Reference to the scene manager
	public GameObject Content;//The Content object on the scroll view where text goes
	public GameObject Menu;
	

	private string fileName;
	public Text textPrefab;

	private List<Rank> rankList = new List<Rank>();

	/// <summary>
	/// This simple struct holds the information for one rank in the leaderboard.
	/// 
	/// </summary>
	public struct Rank
	{
		public string name;
		public int score;

		public Rank(int Score,string Name)
		{
			name = Name;
			score = Score;
		}
	}

	// Use this for initialization
	void Start () {

		//fileName = SceneManager.GetComponent<LevelManager>().ObjWithAudio.GetComponent<AudioSource>().clip.name + "Leaderboard";
				
		//TextAsset leaderBoard = Resources.Load(fileName) as TextAsset;
		////If the file wasn't found
		//if(leaderBoard == null)
		//{
		//	leaderBoard = Resources.Load("DefaultLeaderboard") as TextAsset;
		//}


		////Read the text asset.
		//string lb = leaderBoard.text;
		//string[] LBLines = Regex.Split(lb, @"\n");

		
		////Parse again to get name and score values.
		//for(int i = 0; i < LBLines.Length; i++)
		//{
		   
		//	string[] values = Regex.Split(LBLines[i],";");
			
		//	string text = values[0] + " " + values[1];
		//	//Creating and adding a rank to the list.
		//	Rank newRank = new Rank(int.Parse(values[0]), values[1]);
		//	rankList.Add(newRank);
			
		//}
		//foreach(Rank r in rankList)
		//{
		//	Text newText = Instantiate(textPrefab, transform) as Text;
		//	newText.transform.SetParent(Content.transform, false);
		//	newText.text = r.name + " : " + r.score;
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Switch()
	{
		Menu.SetActive(false);
	}
	/// <summary>
	/// This method is called at the end of the level.
	/// </summary>
	public void UpdateBoard(int newScore)
	{

		//First clear the panel then write it again
		foreach (Transform child in Content.transform)
		{
			Destroy(child);
		}

		//For whatever reason, unity isn't loading the list as expected when update is called.
		//So I'm putting this codeblock here. Mostly to test It shouldn't matter
		//since the file size so small.
		fileName = SceneManager.GetComponent<LevelManager>().ObjWithAudio.GetComponent<AudioSource>().clip.name + "Leaderboard";

		TextAsset leaderBoard = Resources.Load(fileName) as TextAsset;
		//If the file wasn't found
		if (leaderBoard == null)
		{
			leaderBoard = Resources.Load("DefaultLeaderboard") as TextAsset;
		}

		//Read the text asset.
		string lb = leaderBoard.text;
		string[] LBLines = Regex.Split(lb, @"\n");
		//Parse again to get name and score values.
		for (int i = 0; i < LBLines.Length; i++)
		{
			if (LBLines[i].Length != 0)
			{
				string[] values = Regex.Split(LBLines[i], ";");

				string text = values[0] + " " + values[1];
				//Creating and adding a rank to the list.
				Rank newRank = new Rank(int.Parse(values[0]), values[1].TrimEnd('\r', '\n'));
				rankList.Add(newRank); 
			}
		}
		
		//Iterate over the list 
		for(int i = 0; i < rankList.Count; i++)
		{
			//comparing the score given to the score at each rank
			if (rankList[i].score <= newScore)
			{
				//If the score is greater than the current rank, create a new ranking to insert
				Rank newRank = new Rank(newScore, DataManager.data.PlayerName);

				//This will also result in one of the ranks getting pushed off the bottom.
				rankList.Insert(i, newRank);
				rankList.RemoveAt(rankList.Count - 1);

				//We don't want to insert it more than once, so then break from the loop.
				break;
			}
		}

		
		foreach (Rank r in rankList)
		{
			Debug.Log(r.score);
			Text newText = Instantiate(textPrefab, Content.transform) as Text;
			newText.transform.SetParent(Content.transform, false);
			newText.text = r.name + " : " + r.score;
		}

		//And save it to the text file
		Debug.Log(fileName);
		Debug.Log(File.Exists(Application.dataPath + "/Resources/" + fileName + ".txt"));
		if (File.Exists(Application.dataPath + "/Resources/" + fileName + ".txt"))
		{
			File.Delete(Application.dataPath + "/Resources/" + fileName + ".txt");
		}
		using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/" + fileName + ".txt"))
		{
			Debug.Log(rankList.Count);
			foreach (Rank r in rankList)
			{
				writer.WriteLine(r.score + ";" + r.name);
			}
		}
	}
}
