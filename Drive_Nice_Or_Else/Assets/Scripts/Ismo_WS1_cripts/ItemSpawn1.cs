using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
}
