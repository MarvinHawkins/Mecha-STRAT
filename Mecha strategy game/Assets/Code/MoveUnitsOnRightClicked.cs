using UnityEngine;
using System.Collections;

public class MoveUnitsOnRightClicked : MonoBehaviour {

    public Unit_Manager unitmanager;

    // Use this for initialization
    void Start()
    {
     
        unitmanager = GameObject.Find("Player").GetComponent<Unit_Manager>();
    }

    private void RightClicked(Vector3 clickPosition)
    {
        Debug.Log( "in right clicked");
       foreach(GameObject Unit in unitmanager.selectedUnits)
        {
            Debug.Log("ground pos  " + clickPosition + "clicked");
            Unit.SendMessage("MoveOrder", clickPosition, SendMessageOptions.DontRequireReceiver);
        }



    }
}
