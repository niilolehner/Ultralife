using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnObjects : MonoBehaviour
{
    // Takes the class and make it public.
    public static SpawnObjects instance;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public GameObject[] items;
    public GameObject questionItem;
    public GameObject[] roads;
    private int randomSide;
    public float timeBetweenSpawnItems;
    public float timeBetweenSpawnRoads;
    private float spawnTimeItems;
    private float spawnTimeRoads;

    public GameObject[] crossedGameObect;
    public float timeBetweenSpawn;
    float floatRandomX;
    private float spawnTime;

    // Create a temporary reference to the current scene.
    UnityEngine.SceneManagement.Scene currentScene;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Get the current scene.
        currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "EndProduct")
        {
            // Creates delay to spawning random items.
            if (Time.time > spawnTime)
            {
                if (Random.Range(0, 5) == 0)
                {
                    SpawnCrossed(crossedGameObect[Random.Range(0, 2)]);
                }
                else
                {
                    if (Random.Range(0, 3) == 0)
                    {
                        Spawn(questionItem);

                    }
                    else {
                        Spawn(items[Random.Range(0, items.Length)]);
                    }   
                }
                spawnTime = Time.time + timeBetweenSpawn;
            }
        }
        else if (sceneName == "Lvl1")
        {
            if (Time.time > spawnTimeItems)
            {
                int randomItemNumber = Random.Range(0, items.Length);
                SpawnCollectableItems(items[randomItemNumber]);
                spawnTimeItems = Time.time + timeBetweenSpawnItems;
            }

            if (Time.time > spawnTimeRoads)
            {
                SpawnCrossingRoads(roads[0]);
                spawnTimeRoads = Time.time + timeBetweenSpawnRoads + Random.Range(0, 20f);
            }
        }
    }

    // Get object and creates it to random position on the road. 
    void SpawnCollectableItems(GameObject item)
    {
        int randomX = Random.Range(0, 2);

        if (randomX == 0)
        {
            floatRandomX = 1.5f;
        }
        else
        {
            floatRandomX = -1.5f;
        }

        float randomY = Random.Range(-4.7f, 4.7f);

        Instantiate(item, transform.position + new Vector3(floatRandomX, randomY, 0), transform.rotation);
    }

    void SpawnCrossingRoads(GameObject road)
    {
        randomSide = Random.Range(0, 3);

        if (randomSide <= 1)
        {
            Instantiate(road, transform.position + new Vector3(-7f, 15f, 0f), transform.rotation);
        }
        else
        {
            Instantiate(road, transform.position + new Vector3(7.5f, 15f, 0f), transform.rotation);
        }
    }
    
    // Get object and creates it to random position on the road. 
    void Spawn(GameObject item)
    {
        int randomX = Random.Range(0, 2);

        if (randomX == 0)
        {
            floatRandomX = 1.4f;
        }
        else
        {
            floatRandomX = -1.0f;
        }

        float randomY = Random.Range(-4.7f, 4.7f);

        Instantiate(item, transform.position + new Vector3(floatRandomX, randomY, 0), transform.rotation);
    }

    void SpawnCrossed(GameObject item)
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}