using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int randomSide;
    public float timeBetweenSpawnItems;
    public float timeBetweenSpawnRoads;
    private float spawnTimeItems;
    private float spawnTimeRoads;

    public GameObject[] crossedGameObect;
    public float timeBetweenSpawn;
    float floatRandomX;
    private float spawnTime;

    string sceneName;
    List<string> gamePlayAllowed;

    LevelManager levelManager = LevelManager.instance;

    int scenario = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the name of this scene.
        sceneName = SceneManager.GetActiveScene().name;
        gamePlayAllowed = LevelManager.instance.GetGamePlayAllowedUntilActualLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //for  demo
        /*
        if (Time.time > spawnTime)
        {

            switch (scenario)
            {
                case 0:
                    Spawn(questionItem);
                    break;
                case 1:
                    Spawn(questionItem);
                    break;
                case 2:
                    Spawn(items[0]);
                    break;
                case 3:
                    Spawn(items[2]);
                    break;
                case 4:
                    Spawn(items[1]);
                    break;
                case 5:
                    Spawn(items[3]);
                    break;
                case 6:
                    SpawnCrossed(levelManager.GamepPlayGameObject["Pedestrian"]);
                    break;
                case 7:
                    SpawnCrossed(levelManager.GamepPlayGameObject["Pedestrian"]);
                    break;
                case 8:
                    SpawnCrossed(levelManager.GamepPlayGameObject["Traficlight"]);
                    break;
                case 9:
                    SpawnCrossed(levelManager.GamepPlayGameObject["Traficlight"]);
                    break;
            }
            scenario++;

            spawnTime = Time.time + timeBetweenSpawn;
        }*/
        if (sceneName == "EndProduct")
        {
            // Creates delay to spawning random items.
            if (Time.time > spawnTime)
            {
                if (Random.Range(0, 5) == 0)
                {
                    if (gamePlayAllowed.Count > 0) {
                        string gameplay = gamePlayAllowed.OrderBy(e => Random.value).First();
                        if (levelManager.GamepPlayGameObject.ContainsKey(gameplay))
                        {
                            SpawnCrossed(levelManager.GamepPlayGameObject[gameplay]);
                        }
                    }
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
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
        else if (sceneName == "AdditionalProduct_1")
        {
            if (Time.time > spawnTimeItems)
            {
                int randomItemNumber = Random.Range(0, items.Length);
                SpawnCollectableItems(items[randomItemNumber]);
                spawnTimeItems = Time.time + timeBetweenSpawnItems;
            }

            if (Time.time > spawnTimeRoads)
            {
                //SpawnCrossingRoads(roads[0]);
                spawnTimeRoads = Time.time + timeBetweenSpawnRoads + Random.Range(0, 20f);
            }
        }
    }

    // WORKING IN AdditionalProduct_1 Scene
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

    // WORKING IN AdditionalProduct_1 Scene
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