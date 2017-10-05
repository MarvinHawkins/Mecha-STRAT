using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using RTS;

public class UserInput : MonoBehaviour
{
    private Player player;
    public Unit_Manager unitmanager;
    public UI_Manager ui_manager;
    public Unit unit;

    // Use this for initialization
    void Start()
    {
        player = transform.GetComponent<Player>();
        unitmanager = GameObject.Find("Player").GetComponent<Unit_Manager>();
        ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        unit = GameObject.Find("Canvas").GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.human)
        {
            MoveCamera();
            RotateCamera();
            MouseActivity();
        }

    }

    private void MoveCamera()
    {

        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //movement code
        if (xpos >= 0 && xpos < ResourceManager.ScrollWidth)
        {
            movement.x -= ResourceManager.ScrollSpeed;
        }
        else if (xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth)
        {
            movement.x += ResourceManager.ScrollSpeed;
        }

        if (ypos >= 0 && ypos < ResourceManager.ScrollWidth)
        {
            movement.z -= ResourceManager.ScrollSpeed;
        }
        else if (ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth)
        {
            movement.z += ResourceManager.ScrollSpeed;
        }

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        //away from ground movement
        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > ResourceManager.MaxCameraHeight)
        {
            destination.y = ResourceManager.MaxCameraHeight;
        }
        else if (destination.y < ResourceManager.MinCameraHeight)
        {
            destination.y = ResourceManager.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }

    private void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        //Detect if rotation is needed
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) && Input.GetMouseButton(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }
        //Move the object if the origin is different than destination

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }

    }

    //Detects the mouse click
    private void MouseActivity()
    {
        if (Input.GetMouseButton(0)) LeftMouseClick(); //call action if player clicks LMB
        else if (Input.GetMouseButtonDown(1)) RightClick(); //Deselect on click
    }

    private void LeftMouseClick()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameObject hitObject = FindHitObject();
            Vector3 hitPoint = FindHitPoint();
            Debug.Log("You hit a  " + hitObject + " at " + hitPoint);
            //Call the clicked function attached to the object that was hit
            hitObject.transform.gameObject.SendMessage("Clicked", hitPoint, SendMessageOptions.DontRequireReceiver);
            //If the player has a unit selected, clear the list if she clicks the ground again
            if (unitmanager.selectedUnits.Count > 0 && hitObject.name == "Ground")
            {
                unitmanager.deselectUnit();
                ui_manager.selectedTower = null;

                //  Debug.Log("Deselct");
            }       
        }
      

    }

    private void RightClick()
    {
        GameObject hitObject = FindHitObject();
        Vector3 rightHitPoint = FindHitPoint();

        //Call the clicked function attached to the object that was hit
        hitObject.transform.gameObject.SendMessage("RightClicked", rightHitPoint, SendMessageOptions.DontRequireReceiver);
    }

    private GameObject FindHitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) return hit.collider.gameObject;
        return null;
    }

    private Vector3 FindHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) return hit.point;
        return ResourceManager.InvalidPosition;
    }

   
}


  

