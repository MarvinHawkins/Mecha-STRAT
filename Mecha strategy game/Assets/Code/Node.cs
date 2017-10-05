using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Node : MonoBehaviour {

    //The purpose of this is to capture the player action when they click a unit
    //May need to update this to building
    public Transform rallyPoint;  //Each Node has its own rally point When the unit is built, it wil go to thispoint
    //Color selectColor;
    public Renderer rende;
    public UI_Manager uiman;
    public Unit_Manager unit_manager;
    public bool isSelected = false; //Only one building can be selected at once
   

    // Use this for initialization
    void Start () {

        rende = GetComponent<Renderer>();
        uiman = GameObject.Find("Canvas").GetComponent<UI_Manager>(); //get a refference to the buttonscript in scen
        unit_manager = GameObject.Find("Player").GetComponent<Unit_Manager>();

        uiman.objectNameLabel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void OnMouseUp () {

        Debug.Log("Clicked " + gameObject);
        
        uiman.selectedTower = this.gameObject;
        unit_manager.SelectSingleUnit(this.gameObject);
        //Add the tower to USelected tower game object
        //I may change this name
        
    }

    void Update()
    {
        if (unit_manager.IsSelected(gameObject))
        {
            //Turn the build button off
            uiman.buildPanel.gameObject.SetActive(true);
            rende.material.color = Color.white;
            uiman.objectNameLabel.gameObject.SetActive(true);
            uiman.objectNameLabel.GetComponent<Text>().text = gameObject.name;
        }
        else
        {

            uiman.buildPanel.gameObject.SetActive(false);
           

        }

    }
}
