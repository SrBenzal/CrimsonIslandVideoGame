using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{ 
    public int value;
    public bool state;
    private Button button;
    public GameObject game;
    public bool findPair;
    private double time;
    private bool flipInTime; 

	// Use this for initialization
	void Start ()
    {
        state = false;
        findPair = false;
        flipInTime = false;
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        time = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(flipInTime)
        {
            time += Time.deltaTime;
            if (time > 1.5)
            {
                Flip();
                time = 0;
                flipInTime = false;
                game.GetComponent<FindAPair>().SetShowingCard(false);
            }
        
        }
	}
     public void SetValue(int param)
    {
        value = param;
    }
    public void Clicked()
    {
        //Debug.Log("hey lisen!");
        if (!state && !findPair)
        {
            //Debug.Log("agsdaf");
            game.GetComponent<FindAPair>().FlippedCard(gameObject);
        }
    }
    public void Flip()
    {
        //Debug.Log("i'm fliping");
        state = !state;
        ShowCard();
    }
    private void ShowCard()
    {
        if (state)
            GetComponentInChildren<Text>().text = value.ToString();
        else
            GetComponentInChildren<Text>().text = " ";
    }
    public void FlipInTime()
    {
        flipInTime = true;
    }
}
