using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //The purpose of this class is to handle everything related to the player
    //created 8-2-17

    public string username;
    public bool human; //Check to ee if the player is human
    public HUD hud;
    WorldObject SelectedObject { get; set; }

    // Use this for initialization
    void Start () {

        hud = GetComponentInChildren<HUD>();
        	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
