using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit_Manager : MonoBehaviour {

    public List< GameObject> selectedUnits;

    void Start()
    {
        selectedUnits.Clear();
    }

    //Control which tower is selected
    public bool IsSelected(GameObject unit)
    {
        if (selectedUnits.Contains(unit))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SelectSingleUnit(GameObject unit)
    {
        selectedUnits.Clear();
        selectedUnits.Add(unit);
    }

    //clear any units from the list
    public void deselectUnit()
    {
        selectedUnits.Clear();
    }

    public List<GameObject> GetSelectedUnits()
    {
        return selectedUnits;
        //allow other classes to get a list
    }

}
