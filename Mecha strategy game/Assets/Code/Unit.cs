using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    public Transform target;


    //Controller
    public Unit_Manager unitmanager;
    public UI_Manager ui_manager;
   


    public bool isSelected = false;
    public float distanceToGoal;
    public Vector3 myGoal;

    [Header("Attributes")]
    public string unitName; //The name of the unit
    public int unitHitPoints; //Howmuch health the unit has
    public int weaponAttackPower; //The amount of damage dealt on a succesful hit
    public float unitRange = 15f; 
    public float unitSpeed; //How fast the unit moves
   
    public int unitCost; //How much the unit costs
    public float turnSpeed = 10f; //Will turn to look at enemy target

    public float unitFireRate = 1f; //How fast the unit shoots
    public float fireCountdown = 0f;

    [Header("GameObject Fields")]
    public GameObject unitProjectile; // The projectile that the player's weapon uses
    public AudioClip weaponSound;
    public GameObject unitUpgrade;  //This represents the object that the unit turns up to once upgraded  
    public GameObject selectionCircle;
    public Transform firePoint; //Transform where projectile Spawns







    // Use this for initialization
    void Start () {
        myGoal = transform.position;
        unitmanager = GameObject.Find("Player").GetComponent<Unit_Manager>();
        ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        selectionCircle.SetActive(false);

        //start to search fo enemies
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }
    //Need to find warypoint on alive
    //Need to move toward the waypoint
    //Constantly check distance


    public void MoveOrder(Vector3 newGoal)
    {
        Debug.Log(newGoal);
        myGoal = newGoal;
    }

    // Update is called once per frame
    void Update()
    {
        //move to destination based on click

        transform.position += (myGoal - transform.position).normalized * unitSpeed * Time.deltaTime;

        if (unitmanager.IsSelected(gameObject))
        {
            selectionCircle.gameObject.SetActive(true);
            ui_manager.objectNameLabel.gameObject.SetActive(true);
            ui_manager.hpTextLabel.gameObject.SetActive(true);
            ui_manager.objectNameLabel.GetComponent<Text>().text = unitName;
            ui_manager.hpTextLabel.GetComponent<Text>().text = "Hit Points:  " + unitHitPoints;

        }
        else
        {
            selectionCircle.gameObject.SetActive(false);
            ui_manager.objectNameLabel.gameObject.SetActive(false);
            ui_manager.hpTextLabel.gameObject.SetActive(false);

        }
        //Targeting and shooting stuff 
        if (target == null)
            return;
        //Lock on Target
        Vector3 dir = target.position = transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotattion = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime*turnSpeed ).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotattion.y, 0f); 

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f/unitFireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
       GameObject bulletGO = (GameObject) Instantiate(unitProjectile, firePoint.position, firePoint.rotation);
        Bullet_Move bullet = bulletGO.GetComponent<Bullet_Move>();

        //Make sure the bullet scipt is attached
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

        void Clicked()
    {
        Debug.Log("in clicked");
        unitmanager.SelectSingleUnit(this.gameObject);
        //Add the tower to USelected tower game object
        //I may change this name
        ui_manager.selectedTower = this.gameObject;
    }

  

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            //loop through all enemies find the closet one
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //Check if any enemies in array is closer tan infinity
            //if so make the nearest enemy equal to tha enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //if you have an enemy target and it iis in range of th uunit, set the unit's target to this enemy
        if(nearestEnemy != null && shortestDistance <= unitRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    //Visually show range on screen
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, unitRange);
    }


}
