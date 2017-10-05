using UnityEngine;
using System.Collections;

//This script handles keeping track of the enemy waves, 

[System.Serializable]
public class Wave
{
  //  public GameObject[] spawnPoints;
    //Array of waypoints when Unit is alive it will find a spawnpoint and assign its children to waypoints array
    public GameObject enemyPrefab;

    public string name = "";
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}


public class SpawnController : MonoBehaviour
{

    public Wave[] waves;
    public GameObject[] waypoints;
    public int timeBetweenWaves = 5;
    public float lastSpawnTime;
    private int enemiesSpawned = 0;

    public GameController gamecontroller;  //Find the game manager class -hardcoded for now

    // Use this for initialization
    void Awake()
    {


        int i = 0;
        for (i = 0; i < waves.Length; i++)
        {
            name = "Wave " + i;
        }

    }

    void Start()
    {
        lastSpawnTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gamecontroller.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
      timeInterval > spawnInterval) &&
     enemiesSpawned < waves[currentWave].maxEnemies)
            {
                lastSpawnTime = Time.time;
                //Instantiae enemy
                GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab, transform.position, transform.rotation);
                //Assign the enemy to a spawn path 
                //Take the class's waypoint array and aassign the waypoints in the spawncontroller array
                newEnemy.GetComponent<Base_Enemy_Unit>().waypoints = waypoints;
                enemiesSpawned++;
                Debug.Log("Spawn");
            }

            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                  GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                Debug.Log("Next wave");
                gamecontroller.Wave++; //increase to wave
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }





        }



    }
}