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
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
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
            int randomItem = Random.Range(0, items.Length);
            Spawn(items[randomItem]);
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    // Get object and creates it to random position on the road. 
    void Spawn(GameObject item)
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(item, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
