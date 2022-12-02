using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemSpawn1: MonoBehaviour
{
    /// <summary>
    // You can reference/pick up variables and objects of this class by typing: ClassName.instance.TypeHereWhatYouWantToGet
    // for example, PlayerController.instance.GetHealth();
    /// </summary>

    // Takes class and make it public.
    public static ItemSpawn1 instance;

    public BoxCollider2D col;
    public Rigidbody2D rb;

    private float height;
    private float scrollSpeed = -2f;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        //roads = GameObject.FindGameObjectsWithTag("Road");

        height = col.size.y;
        col.enabled = false;

        // Scroll images up to down.
        rb.velocity = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*
     * 
     * 
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
     * 
     * 
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
    */

}
