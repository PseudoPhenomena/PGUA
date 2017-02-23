using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour {

    public Transform BlackObjectPool;
    public Transform WhiteObjectPool;

    public GameObject ActivePool;

    public Transform TopPosition;
    public Transform BotPosition;

    public int MaxDelay = 3;

    private Vector3 Offset = new Vector3(20, 0, 0);

    private System.Random rng;

	// Use this for initialization
	void Start () {
        rng = new System.Random();
	}
	
	// Awake is called once
	void Awake () {
        //start generating tokens
        StartCoroutine("GenerateTokens");
	}

    private IEnumerator GenerateTokens()
    {
        while (true)
        { 
            yield return new WaitForSeconds(1);

            int generationDeterminant = rng.Next(0, 4);

            if (generationDeterminant == 1)
            {
                GenerateTop(rng.Next(0, 2));
            }
            else if (generationDeterminant == 2)
            {
                GenerateBot(rng.Next(0, 2));
            }
            else
            {
                GenerateBoth();
            }
        }
    }


    private void GenerateBoth()
    {
        int color = rng.Next(0, 2);
        GenerateBot(color);
        GenerateTop((color == 1) ? 0 : 1);
    }

    // 1 is black 
    // 0 is white
    private void GenerateTop(int color)
    {
        GameObject token;
        if (color == 1)
        {
            if(BlackObjectPool.childCount > 0)
            {
                token = BlackObjectPool.transform.GetChild(0).gameObject;
                token.transform.parent = ActivePool.transform;
                token.transform.position = TopPosition.position + Offset;
                token.SetActive(true);
            }
        }
        else
        {
            if(WhiteObjectPool.childCount > 0)
            {
                token = WhiteObjectPool.transform.GetChild(0).gameObject;
                token.transform.parent = ActivePool.transform;
                token.transform.position = TopPosition.position + Offset;
                token.SetActive(true);
            }
        }
    }

    // 1 is black
    // 0 is white
    private void GenerateBot(int color)
    {
        GameObject token;
        if (color == 1)
        {
            if (BlackObjectPool.childCount > 0)
            {
                token = BlackObjectPool.transform.GetChild(0).gameObject;
                token.transform.parent = ActivePool.transform;
                token.transform.position = BotPosition.position + Offset;
                token.SetActive(true);
            }
        }
        else
        {
            if (WhiteObjectPool.childCount > 0)
            {
                token = WhiteObjectPool.transform.GetChild(0).gameObject;
                token.transform.parent = ActivePool.transform;
                token.transform.position = BotPosition.position + Offset;
                token.SetActive(true);
            }
        }
    }
}
