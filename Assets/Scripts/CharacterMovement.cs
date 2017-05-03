using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the class that controls all of the movement and works of the characters
/// Note that the character on bottom is always controlled with the same button.
/// The space button is jump.
/// </summary>
public class CharacterMovement : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	//The audio source of the song. Placed her to help map beat maps of songs when I play around with them.
	public GameObject audioSource;

	//The top and bottom Characters are labeled here
	public GameObject topPlayer;
	public GameObject botPlayer;


	//Their direction vectors must be tracked seperately. After all, they are moving independently
	private Vector3 botDirection = Vector3.zero;
	private Vector3 topDirection = Vector3.zero;

	//The controllers likely have to be swapped too
	private CharacterController tPlayer;
	private CharacterController bPlayer;

	//The Song clip itself
	private AudioSource song;

	//The y-levels of both players
	private float topY;
	private float botY;

	//The jump vector
	private Vector3 jump;
	// Use this for initialization
	void Start () {
		tPlayer = topPlayer.GetComponent<CharacterController>();
		bPlayer = botPlayer.GetComponent<CharacterController>();
		topY = tPlayer.transform.position.y;
		botY = tPlayer.transform.position.y;
		song = audioSource.GetComponent<AudioSource>();
		jump = new Vector3(0, jumpSpeed, 0);
	}

	// Update is called once per frame
	void Update() {

		topDirection = new Vector3(1, 0, 0);
        topDirection = transform.TransformDirection(topDirection);
        topDirection *= speed;

		botDirection = new Vector3(1, 0, 0);
        botDirection = transform.TransformDirection(botDirection);
        botDirection *= speed;

		//If space is pressed, swap the players
		if (Input.GetButtonDown("Switch"))
		{
			Swap();
		}

		//JumpCode
		//Move it up when the jump button is pressed.
		if (Input.GetButtonDown("JumpTop"))
		{
			tPlayer.transform.position += jump;            
		}
		//Move it back when it's released
		if (Input.GetButtonUp("JumpTop"))
		{
			tPlayer.transform.position -= jump;
		}
		//Update Top player
		//tPlayer.Move(topDirection * Time.deltaTime);


		//JumpCode
		//Move it up when the jump button is pressed
		if (Input.GetButtonDown("JumpBottom"))
		{
			bPlayer.transform.position += jump;
		}
		//Move it back when it's released
		if (Input.GetButtonUp("JumpBottom"))
		{
			bPlayer.transform.position -= jump;
		}
		//Update Bot player
		//bPlayer.Move(botDirection * Time.deltaTime);
	}  
	
    void FixedUpdate()
    {
        tPlayer.Move(topDirection * Time.deltaTime);
        bPlayer.Move(botDirection * Time.deltaTime);
    }
	/// <summary>
	/// There were a few implementations to consider for this method.
	/// The one I decided to go with should work as follows.
	/// 
	/// 1. Swap the players by position.
	/// 
	/// 2. Swap the controls so that the top and bottom control consistently. This
	/// is done by swapping their names and just using those names to check which
	/// jump to do.
	/// Additionals: For later implementation, consider adding a particle effect
	/// on swap.
	/// </summary>
	private void Swap()
	{
		Vector3 temp = topPlayer.transform.position;
		topPlayer.transform.position = botPlayer.transform.position;
		botPlayer.transform.position = temp;

		string tmp = topPlayer.name;
		topPlayer.name = botPlayer.name;
		botPlayer.name = tmp;

		CharacterController swap = tPlayer;
		tPlayer = bPlayer;
		bPlayer = swap;
	}    
}
