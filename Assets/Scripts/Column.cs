using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Column : MonoBehaviour {

	//The four buttons that the column has
	public Button[] colButtons = new Button[4];
	//Reference to the button prefab
	public GameObject button;
	//The MapManager that updates whenever the column changes
	private GameObject Manager;
	//And this is the item that holds the reference to the actual script. So there aren't a bunch of GetComponents();
	[HideInInspector]
	public MapManager MM;
	//This is the beat number of the column. Being stored so we can update the
	[HideInInspector]
	public int BeatNumber;
	//This is the x-coordinate of every object in this beat
	[HideInInspector]
	public float pos;

	// Use this for initialization
	void Start () {
		Manager = GameObject.Find("MapManager");
		MM = Manager.GetComponent<MapManager>();

		//Instantiate the four buttons and set them in the array
		//for(int i = 0; i<colButtons.Length; i++)
		//{
		//	GameObject newBtn = Instantiate(button);
		//	colButtons[i] = newBtn.GetComponent<Button>();
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// This is the other method that will be called when a button is clicked on
	/// It takes the column as is, and inserts it into the dictionary to overwrite
	/// the column as it was before.
	/// </summary>
	public void ColumnUpdate()
	{
		MM.UpdateMap(BeatNumber, this);
	}
	
}
