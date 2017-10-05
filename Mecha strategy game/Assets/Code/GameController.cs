using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    //Vars
    public int playerCredits;
    public int currentWave; //The wave that the player is on starts at one
    public int maxWave; //The total waves a level can have
    public Text waveLabel;
    public Text creditLabel;

    //property of the money object
    public int Money
    {

        get { return playerCredits; }
        set
        {
            playerCredits = value;
            creditLabel.GetComponent<Text>().text = "Money: " + playerCredits;
        }
    }

    private int wave;
    public int Wave
    {
        get { return wave; }
        set {
            wave = value;   
            waveLabel.text = "WAVE: " + (wave + 1);
        }
    }



    // Use this for initialization
    void Start () {

        Money = playerCredits; //Set the proprty for money
        Wave = 0;



    }

    // Update is called once per frame
    void Update () {
	
	}
}
