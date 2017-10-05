using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class UI_Manager : MonoBehaviour {

    //public GameObject towerMech; was attached to the build button
    public GameObject selectedTower;
    public GameController game_controller; //A call to the game controler object
    public Unit unit; //Call to the unit class

    public List<GameObject> avaiableUnits; //The list of units that the player can build. T


    //UI Labels for interface
    public Text objectNameLabel;
    public Text hpTextLabel;
    public Text atpTextLabel;
    public Text killsLabel;
    public Text weaponsLabel;

    //Buttons
    
    public GameObject buildPanel;
  


    // Use this for initialization
    void Start ()
    {
        //Turn of all UI elements
        objectNameLabel.gameObject.SetActive(false);
        hpTextLabel.gameObject.SetActive(false);
        atpTextLabel.gameObject.SetActive(false);
        killsLabel.gameObject.SetActive(false);
        weaponsLabel.gameObject.SetActive(false);
      
        buildPanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    /*
    public bool MouseInBounds()
    {
        //Screen coordinates start in the lower-left corner of the screen
        //not the top-left of the screen like the drawing coordinates do
        Vector3 mousePos = Input.mousePosition;
        bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - ORDERS_BAR_WIDTH;
        bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height - RESOURCE_BAR_HEIGHT;
        return insideWidth && insideHeight;
    }*/

    //Handles building a unit
   public void Createunit(int newunit)
    {
        // Assign the cost based on what the layer has selected
      int cost = avaiableUnits[newunit].GetComponent<Unit>().unitCost;

        if (game_controller.playerCredits > cost )
        {
            Vector3 spawnPos = selectedTower.transform.position;
            spawnPos.z -= 1;
            game_controller.Money -= cost;
            GameObject newTower = Instantiate(avaiableUnits[newunit], spawnPos, Quaternion.identity) as GameObject;
        }
        

    }
}
