using UnityEngine;
using System.Collections;

public class Bullet_Move : MonoBehaviour {

    public float speed = 10;

    //Code the rest of the variables
    public Transform  target;

    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;



    // Use this for initialization
    void Start () {
       
	
	}
    public void Seek(Transform _target)
    {
        //Grab the target from the Tower or Unit
        target = _target;
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
       
        //control collision
        //Checks to se if bullet has reached its target
        if(dir.magnitude <= distanceThisFrame)
        {
            //If this is true, you hit something
            HitTarget();
            return;
        }
        //Do the actual moving
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
    



}
