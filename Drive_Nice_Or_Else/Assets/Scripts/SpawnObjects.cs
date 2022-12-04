using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject[] crossedGameObect;
    public float timeBetweenSpawn;
    float floatRandomX;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Creates delay to spawning random items.
        if (Time.time > spawnTime)
        {
            if (Random.Range(0, 5) == 0)
            {
                SpawnCrossed(crossedGameObect[Random.Range(0, 2)]);
            }
            else {
                int randomItem = Random.Range(0, items.Length);
                Spawn(items[randomItem]);
            }
            spawnTime = Time.time + timeBetweenSpawn;
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
