using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharAnimations : MonoBehaviour {
    // Speed at which char adjusts
    public float speed = 1;

    // Character
    public GameObject Character;

    // Places the character can be
    public Transform School;
    public Transform RiverCorner;
    public Transform GraveyardIntersection;
    public Transform Arcade;
    public Transform OutsideGraveyard;
    public Transform Graveyard;

    public UnityEngine.UI.Text LocationDisplay;
    [HideInInspector]
    public string CurrentLocation = "Graveyard";

    public Transform target;
    private string endTarget;

    void Start()
    {
        // get the player's last location
        string location = SceneLoadSettings.lastLocation;

        // move them to where they were before
        if(location == "Graveyard")
        {
            LocationDisplay.text = "Graveyard";
            CurrentLocation = "Graveyard";
            target.position = Graveyard.position;
            Character.transform.position = Graveyard.position;
        }
        else if (location == "Arcade")
        {
            LocationDisplay.text = "Arcade";
            CurrentLocation = "Arcade";
            target.position = Arcade.position;
            Character.transform.position = Arcade.position;
        }
        else // school
        {
            LocationDisplay.text = "School";
            CurrentLocation = "School";
            target.position = School.position;
            Character.transform.position = School.position;
        }
    }

    // Update is called once per frame
    void Update () {

        if (Character.transform.position != target.position)
        {
            // move towards the target location
            MoveToTarget();
            // if we reached it after moving
            if (Character.transform.position == target.position)
            {
                UponArival();
            }
        }
        else if (Input.GetButtonDown("Horizontal"))
        {
            // which way are we moving
            float direction = Input.GetAxis("Horizontal");

            StartMoving(direction);
        }
	}

    // sets the initial target
    private void StartMoving(float direction) // negative is left, positive is right
    {
        // where we are determines where we can go
        if (ComparePosWithTollerance(Character.transform, Graveyard))
        {
            // s
            if (direction > 0)
            {
                endTarget = "Arcade";
            }
            // a
            else
            {
                endTarget = "School";
            }
            // always have to go to the same place from graveyard
            target.position = OutsideGraveyard.position;
        }
        else if (ComparePosWithTollerance(Character.transform, School))
        {
            // s
            if (direction > 0)
            {
                endTarget = "Graveyard";
            }
            // a
            else
            {
                endTarget = "Arcade";
            }
            // always have to go to the same place from School
            target.position = RiverCorner.position;
        }
        else // arcade
        {
            // s
            if (direction > 0)
            {
                endTarget = "School";
            }
            // a
            else
            {
                endTarget = "Graveyard";
            }
            // always have to go to the same place from School
            target.position = GraveyardIntersection.position;
        }
    }

    // moves the character toward the next target location
    void MoveToTarget()
    {
        float step = speed * Time.deltaTime;
        Character.transform.position = Vector3.MoveTowards(Character.transform.position, target.position, step);
    }

    // pathing method that sets up UI overlay text or 
    // the next target if the path is not complete 
    // (reached an intermediary location)
    void UponArival()
    {
        // if we have reached an endpoint
        if (ComparePosWithTollerance(Character.transform, Graveyard))
        {
            LocationDisplay.text = "Graveyard";
            CurrentLocation = "Graveyard";
        }
        else if (ComparePosWithTollerance(Character.transform, School))
        {
            LocationDisplay.text = "School";
            CurrentLocation = "School";
        }
        else if (ComparePosWithTollerance(Character.transform, Arcade))
        {
            LocationDisplay.text = "Arcade";
            CurrentLocation = "Arcade";
        }

        // not an endpoint, must be at an intermediary
        else if (ComparePosWithTollerance(Character.transform, RiverCorner))
        {
            if (endTarget == "School")
            {
                target.position = School.position;
            }
            else // headed elsewhere
            {
                target.position = GraveyardIntersection.position;
            }
        }
        else if (ComparePosWithTollerance(Character.transform, GraveyardIntersection))
        {
            if (endTarget == "School")
            {
                target.position = RiverCorner.position;
            }
            else if (endTarget == "Arcade")
            {
                target.position = Arcade.position;
            }
            else // graveyard
            {
                target.position = OutsideGraveyard.position;
            }
        }
        else if (ComparePosWithTollerance(Character.transform, OutsideGraveyard))
        {
            if (endTarget == "Graveyard")
            {
                target.position = Graveyard.position;
            }
            else // headed away from graveyard
            {
                target.position = GraveyardIntersection.position;
            }
        }
        else
        {
            Debug.Log("Outside of tollerance for any known location");
        }
    }

    /// <summary>
    /// Move towards is a peice of garbage and moves it a little ways off from where
    /// you tell it to move to (less than .0001 in any direction)
    /// 
    /// this essentially gives each transform a box of .0001 size and sees if there is a collision
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>a == b</returns>
    bool ComparePosWithTollerance(Transform a, Transform b)
    {
        float tollerance = .0001f;

        // distance from a to b is < .0001f in any direction
        bool x = (a.position.x - b.position.x < tollerance) && (b.position.x - a.position.x < tollerance);
        bool y = (a.position.y - b.position.y < tollerance) && (b.position.y - a.position.y < tollerance);
        bool z = (a.position.z - b.position.z < tollerance) && (b.position.z - a.position.z < tollerance);

        return ( x && y && z);
    }
}
