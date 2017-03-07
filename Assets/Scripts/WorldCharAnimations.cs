using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharAnimations : MonoBehaviour {
    // Speed at which char adjusts
    public float speed = 1;

    // Character
    public GameObject Character;

    // Places the character can be
    public Transform Location0;
    public Transform Location1;
    public Transform Location2;
    public Transform Location3;
    public Transform Location4;
    public Transform Location5;
    public Transform Location6;
    public Transform Location7;

    // where the char is currently
    [HideInInspector]
    public int current = 0;

    // Convenience feilds
    private List<Transform> locations;
    private Transform currentTransform
    {
        get
        {
            if (current < locations.Count && locations[current] != null)
                return locations[current];
            else
            { return gameObject.transform; }
        }
        set { }
    }

    void Start()
    {
        locations = new List<Transform>
        {
            Location0,
            Location1,
            Location2,
            Location3,
            Location4,
            Location5,
            Location6,
            Location7
        };

        Character.transform.position = currentTransform.position;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (current == 7)
                {
                    current = 0;
                }
                else
                {
                    current++;
                }
            }
            else
            {
                if (current == 0)
                {
                    current = 7;
                }
                else
                {
                    current--;
                }
            }
        }

        if (Character.transform != currentTransform)
        {
            float step = speed * Time.deltaTime;
            Character.transform.position = Vector3.MoveTowards(Character.transform.position, currentTransform.position, step);
        }
	}
}
