using UnityEngine;
using System.Collections;

public class Base_Enemy_Unit : MonoBehaviour {

    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;

    // Use this for initialization
    void Start () {

        lastWaypointSwitchTime = Time.time;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Projectile"))
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // 1 
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
        // 2 
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        // 3 
        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                // 3.a 
                //Get the enamy moving between waypoints
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                // TODO: Rotate into move direction
            }
            else {
                // 3.b 
                Debug.Log("Attacking th base!");
               //Destroy(gameObject);
                //Need to update this to have the enemy stop and fire at the Player base instead of 'dying'
                
                // TODO: deduct health
            }
        }

    }
}
